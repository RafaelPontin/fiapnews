using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao;
public class AssinaturaService : ServiceBase<AssinaturaDto, Assinatura, IAssinaturaRepository>, IAssinaturaService
{
    public AssinaturaService(IAssinaturaRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override void ValidarValores(AssinaturaDto dto)
    {
       if(dto == null) _erros.Add("Informe os dados da Assinatura");
        if (dto.TipoAssinatura == 0) _erros.Add("Informe o tipo de assinatura");
        if(dto.TipoPlano == 0) _erros.Add("Informe o Tipo Plano de assinatura");
        if (_erros.Any()) throw new Exception(string.Join("\n", _erros));
    }

    protected override Assinatura DefinirEntidadeAlteracao(Assinatura entidade, AssinaturaDto dto)
    {
       if(entidade == null)
            throw new ArgumentNullException("Assinatura não informada");

        entidade.AlterarTipoAssinatura(dto.TipoAssinatura);
        entidade.CalculaPeriodicidade(dto.TipoPlano);
        entidade.CalculaPreco(dto.Preco);

       return entidade;
    }

    protected override Assinatura DefinirEntidadeInclusao(AssinaturaDto dto)
    {

        if (dto == null) throw new ArgumentNullException("Não foi encontrado o Dto de assinatura");

        return new Assinatura(dto.TipoAssinatura, dto.Preco, dto.TipoPlano);
    }

    protected override void ValidarDelecao(Assinatura entidade)
    {
        if (entidade == null)
            throw new ArgumentNullException(nameof(entidade), "Assinatura informada não encontrada.");
    }

}
