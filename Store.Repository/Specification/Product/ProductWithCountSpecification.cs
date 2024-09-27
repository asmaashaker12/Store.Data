using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.Product
{
    public class ProductWithCountSpecification:BaseSpecifications<Data.Entities.Product>
    {
        public ProductWithCountSpecification(ProductSpecification specs) :
            base(product => (!specs.BrandId.HasValue || product.ProductBrandId == specs.BrandId.Value) &&
             (!specs.TypeId.HasValue || product.ProductTypeId == specs.TypeId.Value)
               && (string.IsNullOrEmpty(specs.Search)||product.Name.ToLower().Contains(specs.Search)))
        {

        }
            

    }
}
