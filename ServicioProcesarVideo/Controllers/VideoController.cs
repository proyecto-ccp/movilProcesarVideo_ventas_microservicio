using Microsoft.AspNetCore.Mvc;
using Videos.Aplicacion.Comandos;
using Videos.Aplicacion.Dto;

namespace ServicioVideo.Controllers
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
        [Route("CargarVideo")]
        [ProducesResponseType(typeof(VideoOut), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),401)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 500)]
        public async Task<IActionResult> CargarVideo([FromBody] VideoIn videoIn)
        {
            try
            {
                var resultado = await _comandosVideo.CargarVideo(videoIn);

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
