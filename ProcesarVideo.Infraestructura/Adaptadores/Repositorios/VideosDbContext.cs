using Microsoft.EntityFrameworkCore;
using Videos.Dominio.Entidades;

namespace Videos.Infraestructura.Adaptadores.Repositorios
{
    public class VideosDbContext : DbContext
    {
        public VideosDbContext(DbContextOptions<VideosDbContext> options) : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }

    }
}
