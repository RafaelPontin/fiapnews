using Dominio.Entidades;
using System.Collections.ObjectModel;

namespace Dominio.ObjetosDeValor
{
    public class RedeSocial : Base
    {
        public string Nome { get; private set; }
        public string Link { get; private set; }
        public ReadOnlyCollection<Autor> Autores { get; set; }
        public RedeSocial()
        {

        }

        public RedeSocial(string descricao) : base()
        {
            DefinirNome(descricao);
        }

        public void DefinirNome(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        private void ValidarNome(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) 
                throw new ArgumentNullException(nameof(descricao), "O Nome da rede social não pode estar vazio ou nulo.");            
        }

        public void DefinirLink(string link)
        {
            ValidarLink(link);
            Link = link;
        }

        private void ValidarLink(string link)
        {
            if (string.IsNullOrWhiteSpace(link)) 
                throw new ArgumentNullException(nameof(link), "O Link da rede social não pode estar vazio ou nulo.");
        }
    }
}
