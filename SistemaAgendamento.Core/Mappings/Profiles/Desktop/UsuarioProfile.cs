using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings.Profiles.Desktop
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioRequest, Usuario>().ReverseMap();
            CreateMap<Usuario, UsuarioResponse>()
    .ForMember(dest => dest.Rotinas, opt => opt.MapFrom(src => src.RotinasPermitidas));

            CreateMap<Rotina, RotinaResponse>();
        }
    }

}
