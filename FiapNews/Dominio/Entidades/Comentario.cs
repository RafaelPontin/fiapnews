namespace Dominio.Entidades
{
public class Comentario : Base
{
    public string Texto { get; private set; }
    public Guid IdUsuario { get; private set; }
    public virtual Usuario Usuario { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Guid IdNoticia { get; private set; }
    public virtual Noticia Noticia { get; private set; }

    public Comentario(string texto, Guid idUsuario, Guid idNoticia, bool assinante)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("O texto do comentário não pode estar vazio ou nulo.", nameof(texto));
        }

        if (texto.Length > 1000)
        {
            throw new ArgumentException($"O texto do comentário deve ter no máximo 1000 caracteres.", nameof(texto));
        }

        if (idUsuario == Guid.Empty)
        {
            throw new ArgumentException("ID do usuário inválido.", nameof(idUsuario));
        }

        if (idNoticia == Guid.Empty)
        {
            throw new ArgumentException("ID da notícia inválido.", nameof(idNoticia));
        }

        if (!assinante)
        {
            throw new InvalidOperationException("Apenas assinantes podem fazer comentários.");
        }

        Id = Guid.NewGuid();
        Texto = texto;
        IdUsuario = idUsuario;
        IdNoticia = idNoticia;
        DataCriacao = DateTime.UtcNow;
        public bool ValidadoPelaModeracao { get; set; }
    }
}
