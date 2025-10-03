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

            CreateMap<Telefone, TelefoneDto>();
            CreateMap<Colaborador, ColaboradorTelefonesDto>()
                .ForMember(dest => dest.Telefones,
                    opt => opt.MapFrom(src => src.ColaboradoresTelefones.Select(ct => ct.Telefone)));
        }
    }
}
