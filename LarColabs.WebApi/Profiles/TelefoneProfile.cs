using AutoMapper;
using LarColabs.WebApi.DTOs;
using LarColabs.WebApi.Models;

namespace LarColabs.WebApi.Profiles
{
    public class TelefoneProfile : Profile
    {
        public TelefoneProfile()
        {
            CreateMap<TelefoneCreateDto, Telefone>();
            CreateMap<TelefoneUpdateDto, Telefone>();

            CreateMap<Telefone, TelefoneReadDto>();
        }
    }
}
