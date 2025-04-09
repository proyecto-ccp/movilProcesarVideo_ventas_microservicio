using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
using ProcesarVideos.Aplicacion.Dto;
using System.Text;
using System.Text.Json;
using Videos.Aplicacion.Comandos;
using Videos.Aplicacion.Dto;

namespace ServicioProcesarVideo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class VideoController : ControllerBase
    {
        private readonly IComandosVideo _comandosVideo;
        public VideoController(IComandosVideo comandosVideo)
        {
            _comandosVideo = comandosVideo;
        }

        //[HttpPost]
        //[Route("ProcesarVideo")]
        //[ProducesResponseType(typeof(VideoOut), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ValidationProblemDetails),401)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        //public async Task<IActionResult> CargarVideo([FromBody] dynamic body)
        //{
        //    string rawJson = body.ToString();
        //    //Console.WriteLine("Mensaje recibido desde Pub/Sub:");
        //    //Console.WriteLine(rawJson);

        //    // Puedes también mapear a un modelo si lo prefieres:
        //    var json = JsonConvert.DeserializeObject<PubSubPushMessage>(rawJson);
        //    var base64Data = json.Message.Data;

        //    // Decodificar el contenido del mensaje:
        //    var decodedBytes = Convert.FromBase64String(base64Data);
        //    var decodedString = Encoding.UTF8.GetString(decodedBytes);

        //    //Console.WriteLine("Mensaje decodificado: " + decodedString);

        //    return Ok(); // ¡IMPORTANTE! GCP espera un 200 OK
        //}

        [HttpPost]
        [Route("ProcesarVideo")]
        [ProducesResponseType(typeof(VideoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> CargarVideo([FromBody] PubSubPushMessage pubSubPushMessage)
        {
            try
            {
                //Console.WriteLine("Mensaje recibido desde Pub/Sub:");
                //Console.WriteLine(pubSubPushMessage.Message);
                //Console.WriteLine("Data:");
                //Console.WriteLine(pubSubPushMessage.Message.Data);
                var decodedBytes = Convert.FromBase64String(pubSubPushMessage.Message.Data);
                //Console.WriteLine("decodedBytes:");
                //Console.WriteLine(decodedBytes);
                var json = Encoding.UTF8.GetString(decodedBytes);
                //Console.WriteLine("json:");
                //Console.WriteLine(json);

                var videoIn = JsonSerializer.Deserialize<VideoIn>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (videoIn == null)
                {
                    Console.WriteLine("Error al deserializar el mensaje");
                }
                /*else 
                {
                    //Console.WriteLine("videoIn Ok");
                    //Console.WriteLine(videoIn.Id);
                    //Console.WriteLine(videoIn.Nombre);
                    //Console.WriteLine(videoIn.Archivo);
                }*/
                    

                var resultado = await _comandosVideo.ProcesarVideo(videoIn);
                //Console.WriteLine("resultado Ok");
                //Console.WriteLine(resultado.Status);
                //Console.WriteLine(resultado.Mensaje);
                //Console.WriteLine(resultado.Resultado);
                if (resultado.Resultado != Videos.Aplicacion.Enum.Resultado.Error)
                    return Ok(resultado);
                else
                    return Problem(resultado.Mensaje, statusCode: (int)resultado.Status, title: resultado.Resultado.ToString(), type: resultado.Resultado.ToString(), instance: HttpContext.Request.Path);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
