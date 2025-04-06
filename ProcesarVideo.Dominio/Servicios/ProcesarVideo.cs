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
        GoogleCredential credential = null;
        public async Task Procesar(Video video)
        {
            getGoogleClient();
            if (ValidarVideo(video))
            {
                string nombreArchivo = video.Nombre.Substring(0, video.Nombre.IndexOf("mp4")) + "jpeg";
                video.FechaActualizacion = DateTime.Now;

                await AlmacenarImagen(video, nombreArchivo);

                //video.UrlImagen = "https://storage.googleapis.com/videos_ccp/" + nombreArchivo;
                video.UrlImagen = "https://storage.googleapis.com/videos_ccp/Imagen.jpg";
                video.EstadoCarga = "Procesado";
                await videoRepositorio.Procesar(video);
            }
            else
            {
                throw new InvalidOperationException("Valores incorrectos para los parametros minimos");
            }
        }

        private void getGoogleClient()
        {
            if (credential == null)
            {
                using (var jsonStream = new FileStream("../../Recursos/experimento-ccp-8172d4037e96.json", FileMode.Open,
                FileAccess.Read, FileShare.Read))
                {
                    credential = GoogleCredential.FromStream(jsonStream);
                }
            }
        }

        private async Task AlmacenarImagen(Video video, string nombreArchivo)
        {
            byte[] binaryData = Convert.FromBase64String(video.Archivo);
            var file = System.Text.Encoding.UTF8.GetBytes(video.Archivo);

            //await Cargar(binaryData, nombreArchivo, "image/jpeg");
            //Stream stream = new MemoryStream(file);

            //using (var videoFrameReader = new VideoFrameReader(stream))
            //{
            //    if (videoFrameReader.Read())
            //    {
            //        using (var frame = videoFrameReader.GetFrame())
            //        {
            //            var image = frame.SaveAsBytes(ImageFormat.Jpg);
            //            await Cargar(image, nombreArchivo, "image/jpeg");
            //        }
            //    }
            //}
        }

        private async Task Cargar(byte[] binaryData, string nombre, string tipo)
        {
            var gcsStorage = StorageClient.Create(credential);
            await gcsStorage.UploadObjectAsync(
                    "videos_ccp",
                    nombre,
                    tipo,
                    new MemoryStream(binaryData),
                    new UploadObjectOptions
                    {
                        PredefinedAcl = PredefinedObjectAcl.PublicRead
                    });
        }

        public bool ValidarVideo(Video video)
        {
            return video.Id != Guid.Empty && !string.IsNullOrEmpty(video.Nombre)  && !string.IsNullOrEmpty(video.Archivo);
        }
    }
}
