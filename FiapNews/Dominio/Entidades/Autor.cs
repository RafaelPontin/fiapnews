namespace Dominio.Entidades
{
    public class Autor : Usuario
    {
        public Autor()
        {

        }
        public Autor(string nome, string login, string senha, string email, string foto, string redeSocias, string descricao) 
            : base(nome, login, senha, email, foto)
        {
            if (!AutorEhValido(redeSocias, descricao))
                throw new ArgumentException($"É necessário informar todos os campos do autor.");

            RedeSocias = redeSocias.Trim();
            Descricao = descricao.Trim();
        }

        public string RedeSocias { get; private set; }
        public string Descricao { get; private set; }
        public virtual ICollection<Noticia>? Noticias { get; private set; }

        public bool AutorEhValido(string redeSocias, string descricao)
        {
            if (string.IsNullOrWhiteSpace(redeSocias))
            {
                _erros.Add("A Rede social do autor não pode estar vazio ou nula.");
            }

            if (string.IsNullOrWhiteSpace(descricao))
            {
                _erros.Add("A descrição do autor não pode estar vazio ou nula.");
            }

            return !_erros.Any();
        }

    }
}
