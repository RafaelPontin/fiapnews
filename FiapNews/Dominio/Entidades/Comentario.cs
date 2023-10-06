using Dominio.Enum;

namespace Dominio.Entidades;

public class Comentario : Base
{
    private const int LIMITE_COMENTARIO = 1000;
    private const int LIMITE_REJEICAO = 500;
    public string Texto { get; private set; }
    public Assinante Assinante { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Noticia Noticia { get; private set; }
    public EstadoValidacaoComentario EstadoValidacao { get; private set; }
    public DateTime? DataValidacao { get; private set; }
    public Administrador? ModeradorResponsavel { get; private set; }
    public string? MotivoRejeicao { get; private set; }

    private IList<string> _erros = new List<string>();

    public Comentario()
    {

    }

    public Comentario(string texto, Assinante assinante, Noticia noticia) : base()
    {
        DataCriacao = DateTime.UtcNow;
        EstadoValidacao = EstadoValidacaoComentario.Pendente;

        DefinirUsuario(assinante);
        DefinirNoticia(noticia);
        DefinirTexto(texto);
    }

    private void DefinirUsuario(Assinante assinante)
    {
        if(assinante == null)
            _erros.Add("Usuário é obrigatório!");
            //throw new ArgumentException(nameof(assinante),"Usuário é obrigatório!");

        if (_erros.Any())
            throw new ArgumentException(string.Join("\n", _erros));

        Assinante = assinante;
    }

    private void DefinirNoticia(Noticia noticia)
    {
        if(noticia == null)
            _erros.Add("Notícia é obrigatória!");
        //throw new ArgumentException(nameof(noticia),"Notícia é obrigatória!");

        if (_erros.Any())
            throw new ArgumentException(string.Join("\n", _erros));

        Noticia = noticia;
    }

    private void DefinirTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            _erros.Add("Texto é obrigatório!");
            //throw new ArgumentException(nameof(texto),"Texto é obrigatório!");

        if (texto.Length > LIMITE_COMENTARIO)
            _erros.Add($"Texto deve ter no máximo {LIMITE_COMENTARIO} caracteres!");
            //throw new ArgumentException(nameof(texto), $"Texto deve ter no máximo {LIMITE_COMENTARIO} caracteres!");

        if (texto.Contains("http://") || texto.Contains("https://") || texto.Contains("www."))
            _erros.Add("Texto não pode conter links!");
        //throw new ArgumentException(nameof(texto),"Texto não pode conter links!");

        if (_erros.Any())
            throw new ArgumentException(string.Join("\n", _erros));

        Texto = texto;
    }

    public void AprovarComentario(Administrador moderador)
    {
        DefinirStatusComentario(moderador, EstadoValidacaoComentario.Aprovado, string.Empty);
    }

    public void ReprovarComentario(Administrador moderador, string motivo)
    {
        DefinirStatusComentario(moderador, EstadoValidacaoComentario.Reprovado, motivo);
    }

    private void DefinirStatusComentario(Administrador moderador, EstadoValidacaoComentario estado, string motivo)
    {
        if (moderador == null)
            _erros.Add("Moderador é obrigatório!");
           //throw new ArgumentException(nameof(moderador),"Moderador é obrigatório!");

        if (EstadoValidacao != EstadoValidacaoComentario.Pendente)
            _erros.Add("Comentário já foi validado!");
            //throw new ArgumentException(nameof(EstadoValidacao),"Comentário já foi validado!");

        if (estado == EstadoValidacaoComentario.Pendente)
            _erros.Add("Estado de validação é obrigatório, e não pode ser definido como pendente!");
            //throw new ArgumentException(nameof(estado),"Estado de validação é obrigatório, e não pode ser definido como pendente!");

        if (estado == EstadoValidacaoComentario.Reprovado && string.IsNullOrWhiteSpace(motivo))
            _erros.Add("Motivo de rejeição é obrigatório!");
            //throw new ArgumentException(nameof(motivo),"Motivo de rejeição é obrigatório!");

        if (motivo.Length > LIMITE_REJEICAO)
            _erros.Add($"Motivo de rejeição deve ter no máximo {LIMITE_REJEICAO} caracteres!");
            //throw new ArgumentException(nameof(motivo),$"Motivo de rejeição deve ter no máximo {LIMITE_REJEICAO} caracteres!");

        if (_erros.Any())
            throw new ArgumentException(string.Join("\n", _erros));

        EstadoValidacao = estado;
        DataValidacao = DateTime.UtcNow;
        ModeradorResponsavel = moderador;
    }
}