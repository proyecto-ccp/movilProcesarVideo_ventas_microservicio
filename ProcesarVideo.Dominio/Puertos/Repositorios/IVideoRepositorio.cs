using Videos.Dominio.Entidades;

namespace Videos.Dominio.Puertos.Repositorios
{
    public interface IVideoRepositorio
    {
        Task Cargar(Video video);
        Task<Video> ObtenerPorId(Guid id);
        Task<List<Video>> ObtenerListado();
    }
}
