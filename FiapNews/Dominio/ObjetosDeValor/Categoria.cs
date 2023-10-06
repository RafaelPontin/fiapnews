using Dominio.Entidades;

namespace Dominio.ObjetosDeValor
{
    public class Categoria : Base
    {
        public string Descricao { get; private set; }
        private const int TAMANHO_DESCRICAO = 100;
        public IReadOnlyCollection<Noticia>? Noticias { get ; }
        public Categoria()
        {
                
        }

        public Categoria(string descricao) : base()
        {
            DefinirDescricao(descricao);
        }

        public void DefinirDescricao(string descricao)
        {
            ValidarDescricao(descricao);
            Descricao = descricao;
        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentNullException("A descricao do comentário não pode estar vazio ou nulo.", nameof(descricao));

            if (descricao.Length >= TAMANHO_DESCRICAO) throw new ArgumentException($"A descricao deve ter no máximo {TAMANHO_DESCRICAO} caracteres.", nameof(descricao));
        }



    }
}
