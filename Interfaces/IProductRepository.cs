using BakeryApi.ViewModels;

namespace BakeryApi.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<ProductViewModel>> GetProducts();
    Task<ProductViewModel> GetProductById(int productId);
    Task<bool> AddProduct(ProductViewModel product);
    Task<bool> UpdateProductPrice(ProductViewModel product);
}
