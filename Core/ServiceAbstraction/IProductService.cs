using Shared;
using Shared.DateTransfrObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? BrandId,int?TypeId, ProductSortingOptions sortingOption);

        Task<ProductDto > GetProductByIdAsync(int id);


        Task<IEnumerable<TypeDto>> GetAllTypesAsync();

        Task<IEnumerable<BrandDto>> GetBrandsAsync();
    }
}
