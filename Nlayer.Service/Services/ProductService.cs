using AutoMapper;
using NLayer.Core.Dtos;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nlayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

     

        public ProductService(NLayer.Core.Repositories.IService<Product> repository, IUnitofwork unitofwork, IProductRepository productRepository, IMapper mapper) : base(repository, unitofwork)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

        public async  Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {

            var product = await _repository.GetProductsWithCategory();
            var productsDto= _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
