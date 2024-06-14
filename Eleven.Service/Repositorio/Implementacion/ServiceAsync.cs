using AutoMapper;
using Eleven.Data.Repositorio.Interfaz;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eleven.Service.Repositorio.Implementacion
{
    public class ServiceAsync<TEntity, TMdo> : IServiceAsync<TEntity, TMdo>
        where TMdo : class, new() where TEntity :  class, new()
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceAsync<TEntity, TMdo>> _logger;
        public ServiceAsync(IRepository<TEntity> repository, IMapper mapper, ILogger<ServiceAsync<TEntity, TMdo>> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TMdo> AddAsync(TMdo tDto)
        {
            try
            {
                _logger.LogInformation("Iniciando service AddAsync ...");
                var entity = _mapper.Map<TEntity>(tDto);
                await _repository.AddAsync(entity);
                tDto = _mapper.Map<TMdo>(entity);
                _logger.LogInformation("Finalizando service AddAsync ...");
                return tDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(await _repository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public IEnumerable<TMdo> GetAll(Expression<Func<TMdo, bool>>? expression = null)
        {
            try
            {
                var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
                return _repository.GetAll(predicate).Select(_mapper.Map<TMdo>).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<List<TMdo>> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync().ToListAsync();
                return _mapper.Map<List<TMdo>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAll");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<TMdo> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                return _mapper.Map<TMdo>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetByIdAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }
        }

        public async Task<TMdo> GetFirstAsync(Expression<Func<TMdo, bool>> expression)
        {
            try
            {
                var predicate = _mapper.Map<Expression<Func<TEntity, bool>>>(expression);
                var entity = await _repository.GetFirstAsync(predicate);
                return _mapper.Map<TMdo>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetFirstAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }

        public async Task UpdateAsync(TMdo entityTDto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(entityTDto);
                await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync");
                throw new Exception("—Bien, Houston, hemos tenido un problema aquí.");
            }

        }
    }
}
