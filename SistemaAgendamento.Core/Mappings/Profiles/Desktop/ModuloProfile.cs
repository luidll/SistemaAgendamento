using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Mappings.Profiles.Desktop
{
    public class ModuloProfile : Profile
    {
        public ModuloProfile()
        {
            CreateMap<ModuloRequest, Modulo>();
            CreateMap<Modulo, ModuloResponse>()
                .ForMember(
                    dest => dest.SistemaNome,
                    opt => opt.MapFrom(src => src.Sistema != null ? src.Sistema.Nome : null)
                );
        }
    }

}
