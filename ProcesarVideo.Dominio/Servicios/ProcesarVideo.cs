using Videos.Dominio.Entidades;
using Videos.Dominio.Puertos.Repositorios;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.VisualBasic;
using System.Text;
using Google.Apis.Auth.OAuth2;

namespace Videos.Dominio.Servicios
{
    public class ProcesarVideo(IVideoRepositorio videoRepositorio)
    {
        private readonly IVideoRepositorio _videoRepositorio = videoRepositorio;
        public async Task Procesar(Video video)
        {
            if (ValidarVideo(video))
            {
                video.FechaActualizacion = DateTime.Now;
                video.UrlImagen = "https://storage.googleapis.com/videos_ccp/" + video.Nombre;
                video.EstadoCarga = "Procesado";
                await videoRepositorio.Procesar(video);
                
            }
            else
            {
                throw new InvalidOperationException("Valores incorrectos para los parametros minimos");
            }
        }

        public bool ValidarVideo(Video video)
        {
            return video.Id != Guid.Empty && !string.IsNullOrEmpty(video.Nombre)  && !string.IsNullOrEmpty(video.Archivo);
        }
    }
}
