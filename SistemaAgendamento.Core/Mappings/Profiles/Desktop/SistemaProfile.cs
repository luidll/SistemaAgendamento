using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings.Profiles.Desktop
{
    public class SistemaProfile : Profile
    {
        public SistemaProfile()
        {
            CreateMap<SistemaRequest, Sistema>();
            CreateMap<Sistema, SistemaResponse>();
        }
    }
}
