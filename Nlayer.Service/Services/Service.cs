
using Microsoft.EntityFrameworkCore;
using Nlayer.Service.Exceptions;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Services
{
    public class Service<T> : NLayer.Core.Services.IService<T> where T : class
    {

        private readonly NLayer.Core.Repositories.IService<T> _repository;
        private readonly IUnitofwork unitofwork;

        public Service(NLayer.Core.Repositories.IService<T> repository, IUnitofwork unitofwork)
        {
            _repository = repository;
            this.unitofwork = unitofwork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await unitofwork.CommitAsync();
            return entity;

        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {

            await _repository.AddRangeAsync(entities);
            await unitofwork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct= await _repository.GetByIdAsync(id);
            if(hasProduct==null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return hasProduct; // controllerda yapmamak için  burada hata fırlattık.
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await unitofwork.CommitAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
            await unitofwork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.UpdateAsync(entity);
            await unitofwork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
           return _repository.Where(expression);
        }
    }
}
