using Dominio.Entidades;

namespace Dominio.ObjetosDeValor
{
    public class Categoria : Base
    {
        public string Descricao { get; private set; } = string.Empty;
        private const int TAMANHO_DESCRICAO = 100;
        private bool Aprovado { get; set; }
        public IReadOnlyCollection<Noticia> Noticias { get; }
        protected Categoria()
        {

        }

        public Categoria(string descricao) : base()
        {
            DefinirDescricao(descricao);
        }
        public Categoria(string descricao, Guid id) : base()
        {
            DefinirIdEntidade(id);
            DefinirDescricao(descricao);            
        }
        public void DefinirDescricao(string descricao)
        {
            ValidarDescricao(descricao);
            Descricao = descricao;
        }

        public void DefinirIdEntidade(Guid id)
        {
            DefinirId(id);
        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentNullException("A descricao da categoria não pode estar vazio ou nulo.", nameof(descricao));

            if (descricao.Length >= TAMANHO_DESCRICAO) throw new ArgumentException($"A descricao deve ter no máximo {TAMANHO_DESCRICAO} caracteres.", nameof(descricao));
        }

        public bool CompararId(Guid id)
        {
            return Id.Equals(id);
        }
    }
}
