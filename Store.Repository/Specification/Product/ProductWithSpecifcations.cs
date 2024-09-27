using Store.Data.Entities;
using Store.Repository.Specification.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class ProductWithSpecifcations : BaseSpecifications<Data.Entities.Product>
    {
        public ProductWithSpecifcations(ProductSpecification specs) :
            base(product => (!specs.BrandId.HasValue || product.ProductBrandId == specs.BrandId.Value) &&
             (!specs.TypeId.HasValue || product.ProductTypeId == specs.TypeId.Value)
            && (string.IsNullOrEmpty(specs.Search)||product.Name.ToLower().Contains(specs.Search))
            )
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            AddOrerBy(x => x.Name);
            ApplyPagination(specs.PageSize * (specs.PageIindex - 1), specs.PageSize);

            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "priceAsc":
                        AddOrerBy(x=>x.Price);
                        break;
                    case "priceDesc":
                        AddOrerByDescinding(x => x.Price);
                        break;
                    default:
                        AddOrerBy(x => x.Name);
                        break;


                }
            }
        }
        public ProductWithSpecifcations(int?Id) :base(product=>product.Id==Id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
            
        }
    }
}
