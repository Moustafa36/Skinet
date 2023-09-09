using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.specification
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams ProductParam) :base(x =>
        (string.IsNullOrEmpty(ProductParam.Search)||x.Name.ToLower().Contains(ProductParam.Search))&&
        (!ProductParam.BrandId.HasValue || x.ProductBrandId==ProductParam.BrandId) &&
        (!ProductParam.TypeId.HasValue || x.ProductTypeId==ProductParam.TypeId)
        )
        {
            
        }
    }
}