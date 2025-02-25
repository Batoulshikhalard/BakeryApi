using BakeryApi.Data;
using BakeryApi.Interfaces;
using BakeryApi.Models;
using BakeryApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly BakeryContext _context;
    public ProductRepository(BakeryContext context)
    {
        _context = context;
    }

    public async Task<bool> AddProduct(ProductViewModel model)
    {
        var product = new Product
        {
            Name = model.Name,
            Price = model.Price,
        };
        await _context.Products.AddAsync(product);
        return await _context.SaveChangesAsync()  > 0 ;
    }

    public async Task<ProductViewModel> GetProductById(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        if (product == null)
            throw new Exception("Product Not found");

        var view = new ProductViewModel
        {
            Id = product.ProductId,
            Name = product.Name,
            Price = product.Price,
        };
        
        return view;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        return await _context.Products.Select(p => new ProductViewModel
        {
            Id = p.ProductId,
            Name = p.Name,
            Price = p.Price,
        }).ToListAsync();
    }

    public async Task<bool> UpdateProductPrice(ProductViewModel model)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x=>x.ProductId == model.Id);
        if (product == null)
            throw new Exception("Product Not found");

        product.Price = model.Price;

        _context.Update(product);
        return await _context.SaveChangesAsync() > 0;
    }
}
