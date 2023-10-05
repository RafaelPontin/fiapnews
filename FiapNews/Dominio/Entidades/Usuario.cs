using Dominio.ObjetosDeValor;

namespace Dominio.Entidades
{
    public abstract class Usuario : Base
    {
        public string Nome { get; protected set; }
        public string Login { get; protected set; }
        public string Senha { get; protected set; }
        public Email Email { get; protected set; }
        public string Foto { get; protected set; }
        protected Usuario()
        {

        }
        public Usuario(string nome, string login, string senha, string email, string foto) : base()
        {
            if (!UsuarioEhValido(nome, login, senha, email, foto))
                throw new ArgumentException($"É necessário informar todos os campos.");

            Nome = nome.Trim();
            Login = login.Trim();
            Senha = senha.Trim();
            Email = new Email(email.Trim());
            Foto = foto.Trim();
        }

        protected IList<string> Erros()
        {
            return _erros;
        }

        private bool UsuarioEhValido(string nome, string login, string senha, string email, string foto)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                _erros.Add("O nome não pode estar vazio ou nulo.");
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                _erros.Add("O login não pode estar vazio ou nulo.");
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                _erros.Add("A senha não pode estar vazio ou nula.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                _erros.Add("O email não pode estar vazio ou nula.");
            }

            if (string.IsNullOrWhiteSpace(foto))
            {
                _erros.Add("A foto não pode estar vazio ou nula.");
            }

            return !_erros.Any();
        }

        public virtual void AlterarSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException($"A senha não pode estar vazio ou nula.", nameof(senha));

            Senha = senha;
        }

        protected IList<string> _erros = new List<string>();
    }
}