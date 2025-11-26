using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings.Profiles.Web
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
