using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.DTOs
{
    public class TagDto : BaseDto
    {
        [Required(ErrorMessage = "Texto é requerido")]
        public string Texto { get; set; }
    }
}
