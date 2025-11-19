using AutoMapper;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;

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
