using Aplicacao;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;

namespace FiapNews.Controllers;
public class NoticiaController : BaseController<Noticia, NoticiaDto, INoticiaService>
{
    public NoticiaController(INoticiaService appService) : base(appService)
    {
    }
}
