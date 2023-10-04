using Dominio.Entidades;

namespace Dominio.ObjetosDeValor
{
    public class Categoria : Base
    {
        public string Descricao { get; private set; }

        public Categoria(string descricao) : base()
        {
            AdicionarDescricao(descricao);
        }


        private void AdicionarDescricao(string descricao)
        {
            ValidarDescricao(descricao);
            Descricao = descricao;
        }

        public void AlterarDescricao(string descricao)
        {
            ValidarDescricao(descricao);
            Descricao = descricao;
        }


        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrEmpty(descricao)) throw new ArgumentNullException("A descricao do comentário não pode estar vazio ou nulo.", nameof(descricao));

            if (descricao.Length >= 100) throw new ArgumentException($"A descricao deve ter no máximo 100 caracteres.", nameof(descricao));
        }



    }
}
