namespace Aplicacao.DTOs
{
    public class AutorDto : UsuarioDto
    {
        public string Descricao { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; } = new List<RedeSocialDto>();
    }

}
