using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>()
    .ForMember(dest => dest.Rotinas, opt => opt.MapFrom(src => src.RotinasPermitidas));

            CreateMap<Rotina, RotinaResponse>();
        }
    }

}
