using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTOs
{
    public class CategoriaDto : BaseDto
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; }
    }
}
