using Dominio.Enum;

namespace Dominio.Entidades;

public class Comentario : Base
{
    private const int LIMITE_COMENTARIO = 1000;
    private const int LIMITE_REJEICAO = 500;
    public string Texto { get; private set; }
    public Usuario Usuario { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Noticia Noticia { get; private set; }
    public EstadoValidacaoComentario EstadoValidacao { get; private set; }
    public DateTime? DataValidacao { get; private set; }
    public Usuario? ModeradorResponsavel { get; private set; }
    public string? MotivoRejeicao { get; private set; }
    public Comentario? ComentarioPai { get; private set; }

    public Comentario(string texto, Usuario usuario, Noticia noticia, Comentario? comentarioPai)
    {
        DataCriacao = DateTime.UtcNow;
        EstadoValidacao = EstadoValidacaoComentario.Pendente;

        DefinirUsuario(usuario);
        DefinirNoticia(noticia);
        DefinirTexto(texto);
        DefinirComentarioPai(comentarioPai);
    }

    private void DefinirComentarioPai(Comentario? comentarioPai)
    {
        if (comentarioPai == null)
            return;

        if (comentarioPai.Noticia.Id != Noticia.Id)
            throw new ArgumentException("Comentário pai deve ser da mesma notícia!");

        if (comentarioPai.ComentarioPai != null)
            throw new ArgumentException("Comentário pai não pode ser resposta de outro comentário!");

        if (comentarioPai.Id == Id)
            throw new ArgumentException("Comentário pai não pode ser o próprio comentário!");

        if (comentarioPai.EstadoValidacao != EstadoValidacaoComentario.Aprovado)
            throw new ArgumentException("Comentário pai deve estar aprovado!");

        ComentarioPai = comentarioPai;
    }

    private void DefinirUsuario(Usuario usuario)
    {
        if (usuario == null)
            throw new ArgumentException("Usuário é obrigatório!");

        Usuario = usuario;
    }

    private void DefinirNoticia(Noticia noticia)
    {
        if (noticia == null)
            throw new ArgumentException("Notícia é obrigatória!");

        Noticia = noticia;
    }

    private void DefinirTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("Texto é obrigatório!");

        if (texto.Length > LIMITE_COMENTARIO)
            throw new ArgumentException($"Texto deve ter no máximo {LIMITE_COMENTARIO} caracteres!");

        if (texto.Contains("http://") || texto.Contains("https://") || texto.Contains("www."))
            throw new ArgumentException("Texto não pode conter links!");

        Texto = texto;
    }

    public void ValidarComentario(Usuario moderador, EstadoValidacaoComentario estado, string motivo)
    {
        if (moderador == null)
            throw new ArgumentException("Moderador é obrigatório!");

        //if (moderador.TipoUsuario != TipoUsuario.Moderador)
        //    throw new ArgumentException("Usuário não é moderador!");

        if (EstadoValidacao != EstadoValidacaoComentario.Pendente)
            throw new ArgumentException("Comentário já validado!");

        if(estado == EstadoValidacaoComentario.Pendente)
            throw new ArgumentException("Estado de validação é obrigatório, e não pode ser definido como pendente!");

        if (estado == EstadoValidacaoComentario.Reprovado && string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException("Motivo de rejeição é obrigatório!");

        if(motivo.Length > LIMITE_REJEICAO)
            throw new ArgumentException($"Motivo de rejeição deve ter no máximo {LIMITE_REJEICAO} caracteres!");

        EstadoValidacao = estado;
        DataValidacao = DateTime.UtcNow;
        ModeradorResponsavel = moderador;
    }
}