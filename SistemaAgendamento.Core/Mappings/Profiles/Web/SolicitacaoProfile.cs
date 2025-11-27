using AutoMapper;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;

namespace SistemaAgendamento.Application.Mappings.Profiles
{
    public class SolicitaacaoProfile : Profile
    {
        public SolicitaacaoProfile()
        {
            CreateMap<Solicitacao, SolicitacaoResponse>()
                .ForMember(dest => dest.SalaNome, opt => opt.MapFrom(src => src.Agendamento.Sala.Nome))
                .ForMember(dest => dest.DataHoraInicio, opt => opt.MapFrom(src => src.DataHoraInicioSolicitada))
                .ForMember(dest => dest.DataHoraFim, opt => opt.MapFrom(src => src.DataHoraFimSolicitada))
                .ForMember(dest => dest.SolicitadoNome, opt => opt.MapFrom(src => src.Solicitado.NomeCompleto))
                .ForMember(dest => dest.SolicitanteNome, opt => opt.MapFrom(src => src.Solicitante.NomeCompleto))
                .ForMember(dest => dest.RespostaSolicitacao, opt => opt.MapFrom(src => src.RespostaObservacao))
                .ReverseMap();

            CreateMap<SolicitacaoRequest, Solicitacao>()
                .ForMember(dest => dest.DataHoraInicioSolicitada, opt => opt.MapFrom(src => src.DataHoraInicioSolicitada))
                .ForMember(dest => dest.DataHoraFimSolicitada, opt => opt.MapFrom(src => src.DataHoraFimSolicitada));
        }
    }
}
