using AutoMapper;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;

namespace SistemaAgendamento.Application.Mappings.Profiles
{
    public class AgendamentoProfile : Profile
    {
        public AgendamentoProfile()
        {
            CreateMap<Agendamento, AgendamentoResponse>()
                .ForMember(dest => dest.SalaNome, opt => opt.MapFrom(src => src.Sala.Nome))
                .ForMember(dest => dest.ResponsavelNome, opt => opt.MapFrom(src => src.Usuario.NomeCompleto))
                .ReverseMap();

            CreateMap<AgendamentoRequest, Agendamento>();
        }
    }
}