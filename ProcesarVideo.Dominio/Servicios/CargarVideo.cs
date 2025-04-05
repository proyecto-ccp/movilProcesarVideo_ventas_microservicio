using Videos.Dominio.Entidades;
using Videos.Dominio.Puertos.Repositorios;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.VisualBasic;
using System.Text;
using Google.Apis.Auth.OAuth2;

namespace Videos.Dominio.Servicios
{
    public class CargarVideo(IVideoRepositorio videoRepositorio)
    {
        private readonly IVideoRepositorio _videoRepositorio = videoRepositorio;
        public async Task Cargar(Video video)
        {
            if (ValidarVideo(video))
            {
                video.Id = Guid.NewGuid();
                video.FechaCreacion = DateTime.Now;
                video.UrlVideo = "https://storage.googleapis.com/videos_ccp/" + video.Nombre;
                await videoRepositorio.Cargar(video);

                GoogleCredential credential = null;
                using (var jsonStream = new FileStream("../../Recursos/experimento-ccp-8172d4037e96.json", FileMode.Open,
                    FileAccess.Read, FileShare.Read))
                {
                    credential = GoogleCredential.FromStream(jsonStream);
                }

                var gcsStorage = StorageClient.Create(credential);
                var file = Encoding.UTF8.GetBytes(video.Archivo);

                await gcsStorage.UploadObjectAsync(
                        "videos_ccp",
                        video.Nombre,
                        "video/mp4",
                        new MemoryStream(file),
                        new UploadObjectOptions
                        {
                            PredefinedAcl = PredefinedObjectAcl.PublicRead
                        });
                
            }
            else
            {
                throw new InvalidOperationException("Valores incorrectos para los parametros minimos");
            }
        }

        public bool ValidarVideo(Video video)
        {
            return video.IdCliente != Guid.Empty && video.IdProducto != 0 && !string.IsNullOrEmpty(video.Nombre) && !string.IsNullOrEmpty(video.UrlVideo) && !string.IsNullOrEmpty(video.Archivo);
        }
    }
}
