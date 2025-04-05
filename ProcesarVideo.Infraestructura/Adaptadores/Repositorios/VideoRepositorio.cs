using Videos.Dominio.Puertos.Repositorios;
using Videos.Dominio.Entidades;
using Videos.Infraestructura.Adaptadores.RepositorioGenerico;

namespace Videos.Infraestructura.Adaptadores.Repositorios
{
    public class VideoRepositorio : IVideoRepositorio
    {
        private readonly IRepositorioBase<Video> _repositorioBase;

        public VideoRepositorio(IRepositorioBase<Video> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }
        public async Task Cargar(Video video)
        {
            await _repositorioBase.Cargar(video);
        }

        public async Task<List<Video>> ObtenerListado()
        {
            return await _repositorioBase.DarListado();
        }

        public async Task<Video> ObtenerPorId(Guid id)
        {
           return await _repositorioBase.BuscarPorLlave(id);
        }
    }
}
