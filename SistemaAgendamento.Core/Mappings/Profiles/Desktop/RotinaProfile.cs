using AutoMapper;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;

namespace SistemaAgendamento.Application.Mappings.Profiles.Desktop
{
    public class RotinaProfile : Profile
    {
        public RotinaProfile()
        {
            CreateMap<Rotina, RotinaResponse>()
                .ForMember(dest => dest.ModuloNome, opt => opt.MapFrom(src => src.Modulo.Nome))
                .ForMember(dest => dest.SistemaNome, opt => opt.MapFrom(src => src.Modulo.Sistema.Nome));

            CreateMap<RotinaRequest, Rotina>();
        }
    }
}

