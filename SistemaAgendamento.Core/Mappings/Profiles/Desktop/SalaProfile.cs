using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings.Profiles.Desktop
{
    public class SalaProfile : Profile
    {
        public SalaProfile()
        {
            CreateMap<SalaRequest, Sala>();
            CreateMap<Sala, SalaResponse>();
        }
    }
}
