using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Enum;

namespace Aplicacao;

public class ComentarioService : ServiceBase<ComentarioDto, Comentario, IRepositoryBase<Comentario>>, IComentarioService
{
    private readonly IMapper _mapper;
    private readonly IComentarioRepository _comentarioRepository;
    private readonly IAdministradorRepository _administradorRepository;

    public ComentarioService(IComentarioRepository comentarioRepository, IMapper mapper, IAdministradorRepository administradorRepository)
        : base(comentarioRepository, mapper)
    {
        _comentarioRepository = comentarioRepository;
        _administradorRepository = administradorRepository;
        _mapper = mapper;
    }

    //Aprovar ou Reprovar comentario
    public async Task Aprovar(Guid idComentario, Guid idAdministrador)
    {
        Comentario comentario = await _comentarioRepository.ObterPorIdAsync(idComentario);
        Administrador administrador = await _administradorRepository.ObterPorIdAsync(idAdministrador);

        comentario.AprovarComentario(administrador);
        await _comentarioRepository.AlterarAsync(comentario);
    }
    public async Task Reprovar(Guid idComentario, Guid idAdministrador, string motivo)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            _erros.Add("Informe o motivo da reprovação.");
        Comentario comentario = await _comentarioRepository.ObterPorIdAsync(idComentario);
        Administrador administrador = await _administradorRepository.ObterPorIdAsync(idAdministrador);

        comentario.ReprovarComentario(administrador, motivo);
        await _comentarioRepository.AlterarAsync(comentario);
    }

    //Todos
    public async Task<IEnumerable<ComentarioDto>> GetAprovados()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Aprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioDto>> GetReprovados()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Reprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioDto>> GetPendentes()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Pendente);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioDto>>(comentarios);
        return comentariosDto;
    }

    //PorNoticia
    public async Task<IEnumerable<ComentarioDto>> GetAprovadosPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia , EstadoValidacaoComentario.Aprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioDto>> GetReprovadosPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia, EstadoValidacaoComentario.Reprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioDto>> GetPendentesPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia, EstadoValidacaoComentario.Pendente);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioDto>>(comentarios);
        return comentariosDto;
    }

    //Validar Comentario
    protected override void ValidarValores(ComentarioDto comentarioDto)
    {
        if (comentarioDto == null)
            _erros.Add("Informe os dados do `comentario.");

        if (string.IsNullOrWhiteSpace(comentarioDto.Texto))
            _erros.Add("Informe a texto do comentario.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));

        return;
    }
}
