using Core.Entities;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public async void AddProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
    }

    public void DeleteProductAsync(Product product)
    {
         context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var products = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
        {
            products = products.Where(x => x.Brand == brand);
        }
        if (!string.IsNullOrWhiteSpace(type))
        {
            products = products.Where(x => x.Type == type);
        }

        products = sort switch
        {
            "priceAsc" => products.OrderBy(x => x.Price),
            "priceDesc" => products.OrderByDescending(x => x.Price),
            _ => products
        };

        return await products.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProductAsync(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
