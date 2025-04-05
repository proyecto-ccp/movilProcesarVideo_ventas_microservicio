using Videos.Aplicacion.Dto;

namespace Videos.Aplicacion.Comandos
{
    public interface IComandosVideo
    {
        Task<BaseOut> ProcesarVideo(VideoIn producto);
    }
}
