using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [Route("ProcesarVideo")]
        [ProducesResponseType(typeof(VideoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> CargarVideo([FromBody] PubSubPushMessage pubSubPushMessage)
        {
            try
            {
                //VideoIn videoIn = new VideoIn();
                var decodedBytes = Convert.FromBase64String(pubSubPushMessage.Message.Data);
                var json = Encoding.UTF8.GetString(decodedBytes);

                var videoIn = JsonSerializer.Deserialize<VideoIn>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var resultado = await _comandosVideo.ProcesarVideo(videoIn);

                if(resultado.Resultado != Videos.Aplicacion.Enum.Resultado.Error)
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
