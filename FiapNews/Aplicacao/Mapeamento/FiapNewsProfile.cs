using Aplicacao.DTOs;
using AutoMapper;
using Dominio.ObjetosDeValor;
using Dominio.Entidades;
using Aplicacao.DTOs.Comentario;

namespace Aplicacao.Mapeamento
{
    public class FiapNewsProfile : Profile
    {
        public FiapNewsProfile()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Comentario, ComentarioDto>().ReverseMap();
            CreateMap<Assinatura, AssinaturaDto>().ReverseMap();
            CreateMap<Autor, AutorDto>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.MapFrom(d => d.Email.EnderecoEmail));
            CreateMap<Administrador, AdministradorDto>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.MapFrom(d => d.Email.EnderecoEmail));
            CreateMap<Assinante, AssinanteDto>()
                .ForMember(x => x.Senha, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.MapFrom(d => d.Email.EnderecoEmail));
            
            CreateMap<AdministradorDto, Administrador>();
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Noticia, NoticiaDto>().ReverseMap();
        }
    }
}
