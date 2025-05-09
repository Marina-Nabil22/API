﻿using AutoMapper;
using DomainLayer.Models;
using Shared.DateTransfrObjects;
using Services.MappingProfiles; 

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>()); 
        CreateMap<ProductType, ProductDto>();
        CreateMap<ProductBrand, BrandDto>();
    }
}
