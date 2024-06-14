using Eleven.Data.Repositorio.Interfaz;
using Eleven.Data.Entidad;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Eleven.Data.Repositorio.Implementacion
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly RepositoryPatternContext RepositoryPatternContext;
        private readonly ILogger<Repository<TEntity>> _logger;
        public Repository(RepositoryPatternContext repositoryPatternDemoContext, ILogger<Repository<TEntity>> logger)
        {
            RepositoryPatternContext = repositoryPatternDemoContext;
            _logger = logger;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            try
            {
                var result = RepositoryPatternContext.Set<TEntity>().AsNoTracking();
                if (expression != null)
                    result = result.Where(expression);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll");
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        public IQueryable<TEntity> GetAllAsync()
        {
            try
            {
                return RepositoryPatternContext.Set<TEntity>().AsNoTracking();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync");
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await RepositoryPatternContext.Set<TEntity>().FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFirstAsync");
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await RepositoryPatternContext.AddAsync(entity);
                await RepositoryPatternContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync");
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public Task DeleteAsync(TEntity entity)
        {
            try
            {
                RepositoryPatternContext.Set<TEntity>().Remove(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync");
                throw new Exception($"{nameof(entity)} could not be deleted: {ex.Message}");
            }
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                RepositoryPatternContext.Update(entity);
                await RepositoryPatternContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync");
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            try
            {
                return await RepositoryPatternContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByIdAsync");
                throw new Exception($"{nameof(id)} could not be retrieved: {ex.Message}");
            }
        }
    }
}
