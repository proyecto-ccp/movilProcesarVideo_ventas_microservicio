using Videos.Dominio.Entidades;

namespace Videos.Infraestructura.Adaptadores.RepositorioGenerico
{
    public interface IRepositorioBase<T> : IDisposable where T : EntidadBase
    {
        Task<T> Cargar(T entity);
        Task<T> BuscarPorLlave(object ValueKey);
        Task<List<T>> DarListado();
    }
}
