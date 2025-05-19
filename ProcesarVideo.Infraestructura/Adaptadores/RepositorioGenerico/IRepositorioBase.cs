using System.Diagnostics.CodeAnalysis;
using Videos.Dominio.Entidades;

namespace Videos.Infraestructura.Adaptadores.RepositorioGenerico
{
    public interface IRepositorioBase<T> : IDisposable where T : EntidadBase
    {
        Task<T> Procesar(T entity);

        [ExcludeFromCodeCoverage]
        Task<T> BuscarPorLlave(object ValueKey);

        [ExcludeFromCodeCoverage]
        Task<List<T>> DarListado();
    }
}
