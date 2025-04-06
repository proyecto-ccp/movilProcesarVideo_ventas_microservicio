﻿using System.Net;
using Videos.Aplicacion.Dto;
using Videos.Aplicacion.Enum;
using Videos.Dominio.Servicios;
using AutoMapper;
using Videos.Dominio.Puertos.Repositorios;
using Videos.Dominio.Entidades;

namespace Videos.Aplicacion.Comandos
{
    public class ManejadorComandos : IComandosVideo
    {
        private readonly ProcesarVideo _cargarVideo;
        private readonly IMapper _mapper;

        public ManejadorComandos(IVideoRepositorio videoRepositorio, IMapper mapper)
        {
            _cargarVideo = new ProcesarVideo(videoRepositorio);
            _mapper = mapper;
        }
        public async Task<BaseOut> ProcesarVideo(VideoIn video)
        {
            BaseOut baseOut = new();

            try
            {
                var videoDominio = _mapper.Map<Video>(video);
                await _cargarVideo.Procesar(videoDominio);
                baseOut.Resultado = Resultado.Exitoso;
                baseOut.Mensaje = "video procesado exitosamente";
                baseOut.Status = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                baseOut.Resultado = Resultado.Error;
                baseOut.Mensaje = ex.Message;
                baseOut.Status = HttpStatusCode.InternalServerError;
            }

            return baseOut;
        }

    }
}
