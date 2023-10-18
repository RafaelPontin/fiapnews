namespace Aplicacao.DTOs.Comentario
{
    public class ComentarioDto
    {
        public string Texto { get; set; }
        public Guid AssinanteId { get; set; }
        public Guid NoticiaId { get; set; }
    }
}
