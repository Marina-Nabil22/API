using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
     class ProductWithBrandAndTypeSpecifications:BaseSpecifications<Product,int>
    {

        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) :
            base(P=>(!queryParams.BrandId.HasValue|| P.BrandId== queryParams.BrandId) &&(!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            && (string.IsNullOrWhiteSpace(queryParams.SearchValue)|| P.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))) {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                    case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;


                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                    default: break;
            }
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
    }
}
