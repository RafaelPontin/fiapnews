﻿using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs.Comentario;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Enum;

namespace Aplicacao;

public class ComentarioService : IComentarioService
{
    private readonly IMapper _mapper;
    private readonly IComentarioRepository _comentarioRepository;
    private readonly IAdministradorRepository _administradorRepository;
    private readonly INoticiaRepository _noticiaRepository;
    private readonly IAssinanteRepository _assinanteRepository;
    protected List<string> _erros;

    public ComentarioService(IComentarioRepository comentarioRepository, IMapper mapper, IAdministradorRepository administradorRepository, INoticiaRepository noticiaRepository, IAssinanteRepository assinanteRepository)
    {
        _comentarioRepository = comentarioRepository;
        _administradorRepository = administradorRepository;
        _noticiaRepository = noticiaRepository;
        _assinanteRepository = assinanteRepository;
        _mapper = mapper;
    }

    //Aprovar ou Reprovar comentario
    public async Task Aprovar(Guid idComentario, Guid idAdministrador)
    {
        Comentario comentario = await _comentarioRepository.ObterPorIdAsync(idComentario);
        Administrador administrador = await _administradorRepository.ObterPorIdAsync(idAdministrador);
        ValidarValores(comentario);

        comentario.AprovarComentario(administrador);
        await _comentarioRepository.AlterarAsync(comentario);
    }
    public async Task Reprovar(Guid idComentario, Guid idAdministrador, string motivo)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            _erros.Add("Informe o motivo da reprovação.");
        Comentario comentario = await _comentarioRepository.ObterPorIdAsync(idComentario);
        Administrador administrador = await _administradorRepository.ObterPorIdAsync(idAdministrador);
        ValidarValores(comentario);

        comentario.ReprovarComentario(administrador, motivo);
        await _comentarioRepository.AlterarAsync(comentario);
    }

    //Todos
    public async Task<IEnumerable<ComentarioRetornoDto>> GetAprovados()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Aprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetReprovados()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Reprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetPendentes()
    {
        var comentarios = await _comentarioRepository.GetComentario(EstadoValidacaoComentario.Pendente);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }

    // PorNoticia
    public async Task<IEnumerable<ComentarioRetornoDto>> GetAprovadosPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia , EstadoValidacaoComentario.Aprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetReprovadosPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia, EstadoValidacaoComentario.Reprovado);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }
    public async Task<IEnumerable<ComentarioRetornoDto>> GetPendentesPorNoticia(Guid idNoticia)
    {
        var comentarios = await _comentarioRepository.GetComentarioPorNoticia(idNoticia, EstadoValidacaoComentario.Pendente);
        var comentariosDto = _mapper.Map<IEnumerable<ComentarioRetornoDto>>(comentarios);
        return comentariosDto;
    }

    //ValidarComentario
    private void ValidarValoresDto(ComentarioDto comentarioDto)
    {
        if (comentarioDto == null)
            _erros.Add("Informe os dados do comentario.");

        if (string.IsNullOrWhiteSpace(comentarioDto.Texto))
            _erros.Add("Informe a texto do comentario.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));

        return;
    }
    private void ValidarValores(Comentario entidade)
    {
        if (entidade == null)
            _erros.Add("Informe os dados do comentario.");

        if (string.IsNullOrWhiteSpace(entidade.Texto))
            _erros.Add("Informe a texto do comentario.");

        if (entidade.Noticia is null)
            _erros.Add("Noticia informada não encontrada.");

        if (entidade.Assinante is null)
            _erros.Add("Assinante informado não encontrado.");

        if (entidade.Assinante.PodeComentar)
            _erros.Add(entidade.Assinante.Nome + " não pode comentar.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));
    }   
    private void ValidarDelecao(Comentario entidade)
    {
        if (entidade == null)
            _erros.Add("Comentario informada não encontrada.");
        
        if (entidade.Noticia is null)
            _erros.Add("Noticia informada não encontrada.");
        
        if (entidade.Assinante is null)
            _erros.Add("Assinante informado não encontrado.");

        if (entidade.Assinante.PodeComentar)
            _erros.Add(entidade.Assinante.Nome + " não pode comentar.");

        if (_erros.Any())
            throw new Exception(string.Join("\n", _erros));
    }

    //MetodosBasicos
    public async Task AdicionarAsync(ComentarioDto dto)
    {
        ValidarValoresDto(dto);
        Noticia noticia = await _noticiaRepository.ObterPorIdAsync(dto.NoticiaId);
        Assinante assinante = await _assinanteRepository.ObterPorIdAsync(dto.AssinanteId);
        Comentario comentario = new(dto.Texto, assinante, noticia);
        ValidarValores(comentario);

        await _comentarioRepository.AdicionarAsync(comentario);
    }
    public async Task<IReadOnlyList<ComentarioRetornoDto>> ObterTodosAsync()
    {
        var comentarios = await _comentarioRepository.ObterTodosAsync();
        return _mapper.Map<IReadOnlyList<ComentarioRetornoDto>>(comentarios);
    }
    public async Task<ComentarioRetornoDto> ObterPorIdAsync(Guid id)
    {
        var comentario = await _comentarioRepository.ObterPorIdAsync(id);
        return _mapper.Map<ComentarioRetornoDto>(comentario);
    }
    public async Task DeletarAsync(Guid id)
    {
        var comentario = await _comentarioRepository.ObterPorIdAsync(id);
        ValidarDelecao(comentario);
    }
}