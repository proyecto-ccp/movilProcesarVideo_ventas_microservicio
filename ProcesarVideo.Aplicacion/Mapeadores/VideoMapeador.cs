using AutoMapper;
using Videos.Dominio.Entidades;
using Videos.Aplicacion.Dto;

namespace Videos.Aplicacion.Mapeadores
{
    public class VideoMapeador: Profile
    {
        public VideoMapeador() 
        {
           CreateMap<Video, VideoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.UrlImagen, opt => opt.MapFrom(src => src.UrlImagen))
                .ForMember(dest => dest.EstadoCarga, opt => opt.MapFrom(src => src.EstadoCarga))
                .ReverseMap();

            CreateMap<Video,VideoIn>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Archivo, opt => opt.MapFrom(src => src.Archivo))
                .ReverseMap();

            CreateMap<VideoOut,VideoIn>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Video.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Video.Nombre))
                .ReverseMap();
        }
    }
}
