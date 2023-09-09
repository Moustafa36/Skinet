using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.specification
{
    public class ProductsWithTypesAndBrandsSpecification  : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification( ProductSpecParams ProductParam) 
        :base(x =>
        (string.IsNullOrEmpty(ProductParam.Search)||x.Name.ToLower().Contains(ProductParam.Search))&&
        (!ProductParam.BrandId.HasValue || x.ProductBrandId==ProductParam.BrandId) &&
        (!ProductParam.TypeId.HasValue || x.ProductTypeId==ProductParam.TypeId)
        )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            ApplyingPaging(ProductParam.PageSize*(ProductParam.PageIndex-1) , ProductParam.PageSize);
           // AddOrderBy(n=>n.Name);
            if(!string.IsNullOrEmpty(ProductParam.Sort))
            {
                switch (ProductParam.Sort)
                {
                    case "priceAsc": AddOrderBy(p=>p.Price);break;
                    case "priceDesc": AddOrderByDescending(p=>p.Price);break;
                    default:AddOrderBy(n=>n.Name);break;
                };
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id)
        {
             AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}