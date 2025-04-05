using Videos.Dominio.Entidades;

namespace Videos.Dominio.Puertos.Repositorios
{
    public interface IVideoRepositorio
    {
        Task Procesar(Video video);
    }
}
