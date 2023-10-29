using Aplicacao.Contratos.Persistencia;
using Aplicacao.Contratos.Servico;
using Aplicacao.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.ObjetosDeValor;
using static System.Net.Mime.MediaTypeNames;

namespace Aplicacao;
public class NoticiaService : ServiceBase<NoticiaDto, Noticia, INoticiaRepository>, INoticiaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IAutorRepository _autorRepository;
    public NoticiaService(INoticiaRepository repository, IMapper mapper, ICategoriaRepository categoriaRepository, IAutorRepository autorRepository) : base(repository, mapper)
    {
        _categoriaRepository = categoriaRepository;
        _autorRepository = autorRepository;
    }

    protected override Noticia DefinirEntidadeAlteracao(Noticia entidade, NoticiaDto dto)
    {
       if(entidade == null) throw new ArgumentNullException(nameof(entidade), "Noticia informada não encontrada.");

        
        entidade.DefinirTitulo(dto.Titulo);
        entidade.DefinirSubtitulo(dto.SubTitulo);
        entidade.DefinirConteudo(dto.Conteudo);
        entidade.DefinirLead(dto.Lead);
        
        if (dto.Categorias is not null)
        {
            var categorias = CriarCategoria(dto);
            entidade.LimparCategoria();
            entidade.AdicionarCategorias(categorias);
        }
        
        if(dto.Autores is not null)
        {
            var autores = CriarAutor(dto);
            entidade.LimparAutores();
            entidade.AdicionarAutores(autores);
        }
        
        entidade.DefinirRegiao(dto.Regiao);
        dto.Imagens.ToList().ForEach(i => entidade.AdicionarImagem(i)); 

        return entidade;
    }

    private ICollection<Categoria> CriarCategoria(NoticiaDto dto)
    {
        if(dto.Categorias is null) return null; 
        ICollection<Categoria> categorias = new List<Categoria>();
        foreach (var item in dto.Categorias)
        {
            if (item != null)
            {
                Categoria categoria = _categoriaRepository.ObterPorIdAsync(item.Id).Result;
                if (categoria == null)
                    throw new Exception ($"Categoria informada não encontrada. Id: {item.Id}");
                else
                  categorias.Add(categoria);
            }
        }
        return categorias;
    }

    private ICollection<Autor> CriarAutor(NoticiaDto dto)
    {
        if (dto.Autores is null) return null;
        ICollection<Autor> autores = new List<Autor>();
        foreach (var item in dto.Autores)
        {
            if (item != null)
            {
                Autor autor = _autorRepository.ObterAutorPorId(item.Id).Result;
                if (autor == null)
                    throw new Exception($"Autor informado não encontrado. Id: {item.Id}");
                else
                    autores.Add(autor);
            }
        }
        return autores;
    }

    protected override Noticia DefinirEntidadeInclusao(NoticiaDto dto)
    {
        ICollection<Categoria> categorias = CriarCategoria(dto);
        ICollection<Autor> autores = CriarAutor(dto);

        return new Noticia(dto.Titulo, dto.SubTitulo, dto.Conteudo, dto.Lead, categorias , autores, dto.Regiao, false, dto.Imagens);
    }

    protected override void ValidarDelecao(Noticia entidade)
    {
        if (entidade == null)
            throw new ArgumentNullException(nameof(entidade), "Noticia informada não encontrada.");
    }
    protected override void ValidarValores(NoticiaDto dto)
    {
        if (dto == null) _erros.Add("Informe os dados da Noticia!");
        if (string.IsNullOrWhiteSpace(dto.Conteudo)) _erros.Add("informe o conteudo da noticia");
        if (string.IsNullOrWhiteSpace(dto.Titulo)) _erros.Add("informe o titulo da noticia");
        if (string.IsNullOrWhiteSpace(dto.SubTitulo)) _erros.Add("informe o sub titulo da noticia");
        if (string.IsNullOrWhiteSpace(dto.Lead)) _erros.Add("informe o lead da noticia");
        if (dto.DataCriacao == null) _erros.Add("informe o Data criação da noticia");
        if (!dto.Categorias.Any()) _erros.Add("Informe uma categoria");
        if (!dto.Autores.Any()) _erros.Add("Informe uma autor");
        if (_erros.Any()) throw new Exception(string.Join("\n", _erros));
    }

    public IList<NoticiaDto> ObterNoticiaCategoria(Guid idCategoria)
    {
        var noticias = Repository.ObterNoticiaCategoria(idCategoria);
        IList<NoticiaDto> dtos = new List<NoticiaDto>();

        foreach (var item in noticias)
        {
            var noticia = _mapper.Map<NoticiaDto>(item);
            dtos.Add(noticia);
        }
        return dtos;
    }
}
