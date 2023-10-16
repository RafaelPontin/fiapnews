using Aplicacao.DTOs;
using AutoMapper;
using Dominio.ObjetosDeValor;
using Dominio.Entidades;
namespace Aplicacao.Mapeamento
{
    public class FiapNewsProfile : Profile
    {
        public FiapNewsProfile()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
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
            
        }
    }
}
