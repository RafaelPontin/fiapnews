using Dominio.Enum;

namespace Aplicacao.DTOs;

public class ComentarioDto : BaseDto
{
    public string Texto { get; set; }
    public Guid AssinanteId { get; set; }
    public Guid NoticiaId { get; set; }
}
