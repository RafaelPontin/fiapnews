using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao
{
    public class AdministradorService : ServiceBase<AdministradorDto, Administrador, IRepositoryBase<Administrador>>, IAdministradorService
    {
        public AdministradorService(IRepositoryBase<Administrador> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        protected override Administrador DefinirEntidadeInclusao(AdministradorDto dto)
        {
            return new Administrador(dto.Nome, dto.Login, dto.Senha, dto.Email, dto.Foto);
        }

        protected override void ValidarValores(AdministradorDto dto)
        {
            if (dto == null)
                _erros.Add("Informe os dados do administrador.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                _erros.Add("Informe a descrição do administrador.");

            if (_erros.Any())
                throw new Exception(string.Join("\n", _erros));

            return;
        }

        protected override Administrador DefinirEntidadeAlteracao(Administrador entidade, AdministradorDto dto)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Administrador informada não encontrado.");
            

            return entidade;
        }
        protected override void ValidarDelecao(Administrador entidade)
        {
            if (entidade == null)
                throw new ArgumentNullException(nameof(entidade), "Administrador informada não encontrado.");
        }
    }

}
