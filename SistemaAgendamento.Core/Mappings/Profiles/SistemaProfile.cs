using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Mappings
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
