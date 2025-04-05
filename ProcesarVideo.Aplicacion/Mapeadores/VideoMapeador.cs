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
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.UrlVideo, opt => opt.MapFrom(src => src.UrlVideo))
                .ForMember(dest => dest.UrlImagen, opt => opt.MapFrom(src => src.UrlImagen))
                .ForMember(dest => dest.EstadoCarga, opt => opt.MapFrom(src => src.EstadoCarga))
                .ReverseMap();

            CreateMap<Video,VideoIn>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.IdCliente))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.IdProducto))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Archivo))
                //.ForMember(dest => dest.Ruta, opt => opt.MapFrom(src => src.Ruta))
                //.ForMember(dest => dest.UrlVideo, opt => opt.MapFrom(src => src.UrlVideo))
                //.ForMember(dest => dest.UrlImagen, opt => opt.MapFrom(src => src.UrlImagen))
                //.ForMember(dest => dest.EstadoCarga, opt => opt.MapFrom(src => src.EstadoCarga))
                .ReverseMap();

            CreateMap<VideoOut,VideoIn>()
                //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Video.Id))
                .ForMember(dest => dest.IdCliente, opt => opt.MapFrom(src => src.Video.IdCliente))
                .ForMember(dest => dest.IdProducto, opt => opt.MapFrom(src => src.Video.IdProducto))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Video.Nombre))
                //.ForMember(dest => dest.UrlVideo, opt => opt.MapFrom(src => src.Video.UrlVideo))
                //.ForMember(dest => dest.UrlImagen, opt => opt.MapFrom(src => src.Video.UrlImagen))
                //.ForMember(dest => dest.EstadoCarga, opt => opt.MapFrom(src => src.Video.EstadoCarga))
                .ReverseMap();
        }
    }
}
