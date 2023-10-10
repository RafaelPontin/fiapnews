using Aplicacao.DTOs;
using AutoMapper;
using Dominio.ObjetosDeValor;

namespace Aplicacao.Mapeamento
{
    public class FiapNewsProfile : Profile
    {
        public FiapNewsProfile()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
        } 
    }
}
