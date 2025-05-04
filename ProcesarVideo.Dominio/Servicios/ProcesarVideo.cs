using Videos.Dominio.Entidades;
using Videos.Dominio.Puertos.Repositorios;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;
using System.Text.Json;

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

                video.UrlImagen = "https://storage.googleapis.com/videos_ccp/" + nombreArchivo;
                //video.UrlImagen = "https://storage.googleapis.com/videos_ccp/Imagen.jpg";
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
                using (var jsonStream = new FileStream("Recursos/experimento-ccp-8172d4037e96.json", FileMode.Open,
                FileAccess.Read, FileShare.Read))
                {
                    credential = GoogleCredential.FromStream(jsonStream);
                }
            }
        }

        private async Task AlmacenarImagen(Video video, string nombreArchivo)
        {
            // 1. Decodificar Base64 a bytes
            byte[] videoBytes = Convert.FromBase64String(video.Archivo);

            // 2. Guardar el archivo de video temporalmente
            string tempVideoPath = Path.Combine(Path.GetTempPath(), "tempVideo.mp4");
            File.WriteAllBytes(tempVideoPath, videoBytes);

            // 2. Configurar FFmpeg (descarga automática)
            await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, Path.Combine(Environment.CurrentDirectory, "Recursos/ffmpeg"));
            //FFmpeg.SetExecutablesPath("Recursos/ffmpeg");
            FFmpeg.SetExecutablesPath(Path.Combine(Environment.CurrentDirectory, "Recursos/ffmpeg"));

            // 3. Ruta para imagen extraída
            string imagePath = Path.Combine(Path.GetTempPath(), "frame.jpg");

            // 4. Crear conversión para snapshot en el segundo 1
            var conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(tempVideoPath, imagePath, TimeSpan.FromSeconds(1));
            await conversion.Start();

            // 5. Convertir imagen a Base64
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);

            // 7. Limpiar archivo temporal si querés
            File.Delete(tempVideoPath);

            await Cargar(imageBytes, nombreArchivo, "image/jpeg");
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
