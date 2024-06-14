using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eleven.Service.Repositorio.Interfaz
{
    public interface IServiceAsync<TEntity, TMdo>
        where TMdo : class, new() where TEntity : class, new()
    {
        Task<TMdo> AddAsync(TMdo tDto);
        Task DeleteAsync(int id);
        IEnumerable<TMdo> GetAll(Expression<Func<TMdo, bool>>? expression = null);
        Task<List<TMdo>> GetAllAsync();
        //Task<TDto> GetByIdAsync(int id);
        Task UpdateAsync(TMdo entityTDto);
        //Task<TDto> GetFirstAsync(Expression<Func<TDto, bool>> expression);
        Task<TMdo> GetFirstAsync(Expression<Func<TMdo, bool>> expression);
    }
}
