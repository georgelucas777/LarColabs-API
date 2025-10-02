using AutoMapper;
using LarColabs.WebApi.DTOs;
using LarColabs.WebApi.Models;

namespace LarColabs.WebApi.Profiles
{
    public class ColaboradorProfile : Profile
    {
        public ColaboradorProfile()
        {
            CreateMap<ColaboradorCreateDto, Colaborador>();
            CreateMap<ColaboradorUpdateDto, Colaborador>();

            CreateMap<Colaborador, ColaboradorReadDto>();
        }
    }
}
