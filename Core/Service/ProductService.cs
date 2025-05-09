﻿using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DateTransfrObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly  IunitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // Constructor
        public ProductService(IunitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product,int>().GetAllAsync();
            return _mapper .Map<IEnumerable<Product>,IEnumerable<ProductDto>>(Products);
 
    }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types=await _unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            var TypesDto =_mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
        var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
        var Brands = await Repo.GetAllAsync();
        var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);

        return BrandsDto;
    }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Product =await _unitOfWork.GetRepository<Product, int>().GetbyIdAsync(id);
            return _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
