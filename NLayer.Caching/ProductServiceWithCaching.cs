using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.Dtos;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IProductRepository _productService;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly IUnitofwork _unitofwork;

        public ProductServiceWithCaching(IProductRepository productService, IMemoryCache memoryCache, IMapper mapper, IUnitofwork unitofwork)
        {
            _productService = productService;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _unitofwork = unitofwork;

            if (! _memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _productService.GetAll().ToList());
            }

        }
        // Cachelemek ?????????
        public async Task<Product> AddAsync(Product entity)
        {
            await _productService.AddAsync(entity);
            await _unitofwork.CommitAsync();
            CacheAllProducts();
            return entity;
        }

        public Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }
        public  void CacheAllProducts()
        {
            _memoryCache.Set(CacheProductKey, _productService.GetAll().ToList());
        }
    }
}
