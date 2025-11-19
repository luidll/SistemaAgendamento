using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings.Profiles
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
