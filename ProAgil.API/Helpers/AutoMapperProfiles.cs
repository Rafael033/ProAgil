using System.Linq;
using AutoMapper;
using ProAgil.API.Dtos;
using ProAgil.Domain;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfiles : Profile // AutoMapper referenciado no Startup
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento,EventoDto>()
               .ForMember(destinatario => destinatario.Palestrantes, opt => {
               opt.MapFrom(fonte => fonte.PalestranteEventos.Select(x => x.Palestrante).ToList()); //Dto a parte de viewer 
               })
               .ReverseMap();
            CreateMap<Palestrante,PalestranteDto>()
            .ForMember(detinatario => detinatario.Eventos, opt =>{
               opt.MapFrom(fonte => fonte.PalestranteEventos.Select(x => x.Evento).ToList()); 
            })
            .ReverseMap();
            CreateMap<Lote,LoteDto>().ReverseMap();
            CreateMap<RedeSocial,RedeSocialDto>().ReverseMap();
        }
    }
}