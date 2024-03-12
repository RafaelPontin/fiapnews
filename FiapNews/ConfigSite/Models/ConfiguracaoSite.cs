using System.Text.RegularExpressions;

namespace ConfigSite.Models
{
    public class ConfiguracaoSite
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }

        public string RemoverEspacos(string source)
        {
            return Regex.Replace(source, @"\s", string.Empty);
        }

        public bool PossuiCaracteresEspeciais()
        {
            return Regex.IsMatch(Descricao, "[a-z0-9 ]+", RegexOptions.IgnoreCase);
        }

        public void DefinirLink()
        {
            Link = $"http://fiapnews.com.br/{RemoverEspacos(Descricao).ToLower()}";
        }


    }
}
