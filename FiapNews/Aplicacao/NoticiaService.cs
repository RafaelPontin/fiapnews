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
    public NoticiaService(INoticiaRepository repository, IMapper mapper) : base(repository, mapper)
    {
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
            categorias.ToList().ForEach(c => entidade.AdicionarCategoria(c));
        }

        if(dto.Autores is not null)
        {
            var autores = CriarAutor(dto);
            autores.ToList().ForEach(a => entidade.AdicionarAutor(a));
        }

        entidade.DefinirRegiao(dto.Regiao);
        dto.Imagens.ToList().ForEach(i => entidade.AdicionarImagem(i)); 

        return entidade;
    }

    private ICollection<Categoria> CriarCategoria(NoticiaDto dto)
    {
        if(dto.Categorias is null) return null; 
        ICollection<Categoria> categorias = new List<Categoria>();
        foreach (var categoria in dto.Categorias)
        {
            if (categoria != null)
            {
                categorias.Add(new Categoria(categoria.Descricao));
            }
        }
        return categorias;
    }

    private ICollection<Autor> CriarAutor(NoticiaDto dto)
    {
        if (dto.Autores is null) return null;
        ICollection<Autor> autores = new List<Autor>();
        foreach (var autor in dto.Autores)
        {
            if (autor != null)
            {
                autores.Add(new Autor(autor.Nome, autor.Login, autor.Senha, autor.Email, autor.Foto, autor.Descricao, null));
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
}
