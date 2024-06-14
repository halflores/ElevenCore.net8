using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Data.Repositorio.Interfaz
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
        IQueryable<TEntity> GetAllAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity?> GetByIdAsync(int id);
    }

}
