using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using Dominio.Entidades;

namespace FiapNews.Controllers;
public class AssinaturaController : BaseController<Assinatura, AssinaturaDto, IAssinaturaService>
{
    public AssinaturaController(IAssinaturaService appService) : base(appService)
    {
    }
}
