using Videos.Dominio.Puertos.Repositorios;
using Videos.Dominio.Entidades;
using Videos.Infraestructura.Adaptadores.RepositorioGenerico;
using System.Diagnostics.CodeAnalysis;

namespace Videos.Infraestructura.Adaptadores.Repositorios
{
    public class VideoRepositorio : IVideoRepositorio
    {
        private readonly IRepositorioBase<Video> _repositorioBase;

        public VideoRepositorio(IRepositorioBase<Video> repositorioBase)
        {
            _repositorioBase = repositorioBase;
        }
        public async Task Procesar(Video video)
        {
            await _repositorioBase.Procesar(video);
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<Video>> ObtenerListado()
        {
            return await _repositorioBase.DarListado();
        }

        [ExcludeFromCodeCoverage]
        public async Task<Video> ObtenerPorId(Guid id)
        {
           return await _repositorioBase.BuscarPorLlave(id);
        }
    }
}
