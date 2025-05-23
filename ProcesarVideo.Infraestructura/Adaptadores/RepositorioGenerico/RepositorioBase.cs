﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Videos.Dominio.Entidades;
using Videos.Infraestructura.Adaptadores.Repositorios;

namespace Videos.Infraestructura.Adaptadores.RepositorioGenerico
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadBase
    {
        private readonly IServiceProvider _serviceProvider;
        public RepositorioBase(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private VideosDbContext GetContext()
        {
            return _serviceProvider.GetService<VideosDbContext>();
        }

        protected DbSet<T> GetEntitySet()
        {
            return GetContext().Set<T>();
        }

        [ExcludeFromCodeCoverage]
        public async Task<T> Cargar(T entity)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Procesar(T entity)
        {
            try
            {
                var _context = GetContext();
                var entitySet = _context.Set<T>();
                var res = entitySet.Update(entity);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return null;
            }
        }

        [ExcludeFromCodeCoverage]
        public async Task<T> BuscarPorLlave(object ValueKey)
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.FindAsync(ValueKey);
            await _context.DisposeAsync();
            return res;

        }

        [ExcludeFromCodeCoverage]
        public async Task<List<T>> DarListado()
        {
            var _context = GetContext();
            var entitySet = _context.Set<T>();
            var res = await entitySet.ToListAsync();
            await _context.DisposeAsync();
            return res;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                try
                {
                    var ctx = GetContext();
                    ctx.Dispose();
                }
                catch (Exception ex)
                { }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
