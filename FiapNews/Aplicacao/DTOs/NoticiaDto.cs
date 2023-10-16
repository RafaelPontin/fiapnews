using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.DTOs;
public class NoticiaDto : BaseDto
{
    [Required(ErrorMessage = "Titulo obrigatorio")]
    [MaxLength(250, ErrorMessage = "Titulo tem que ser Maior que 250")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "SubTitulo obrigatorio")]
    [MaxLength(120, ErrorMessage = "SubTitulo tem que ser Maior que 250")]
    public string SubTitulo { get; set; }

    [Required(ErrorMessage = "Conteudo obrigatorio")]
    [MaxLength(10000, ErrorMessage = "Conteudo maior que 10000")]
    public string Conteudo { get; set; }

    [Required(ErrorMessage = "Lead Requerido")]
    [MaxLength(1000, ErrorMessage = "Lead não pode ser maior que 1000")]
    public string Lead { get; set; }

    [Required]
    public DateTime DataCriacao { get; set; }

    //feature/elielson
    //public IEnumerable<AutorDto>

    public IEnumerable<CategoriaDto> Categorias { get; set; }
    public string Regiao { get; set; }
    public string LinkDeCompartilhamento { get; set; }

    public IEnumerable<string> Imagens { get; set; }



}
