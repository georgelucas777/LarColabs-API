using AutoMapper;
using LarColabs.WebApi.DTOs;
using LarColabs.WebApi.Models;

namespace LarColabs.WebApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<UsuarioUpdateDto, Usuario>();

            CreateMap<Usuario, UsuarioReadDto>();
        }
    }
}
