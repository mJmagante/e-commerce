using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams productSpecParams) : base(  x =>
        (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
        (productSpecParams.Brands.Count == 0 || productSpecParams.Brands.Contains(x.Brand)) &&
        (productSpecParams.Types.Count == 0 || productSpecParams.Types.Contains(x.Type))
    )
    {
        ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);

        switch (productSpecParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
}
