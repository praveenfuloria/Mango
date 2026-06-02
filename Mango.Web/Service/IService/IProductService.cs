using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponceDto?> GetProductByIdAsync(int id);
        Task<ResponceDto?> GetAllProductsAsync();
        Task<ResponceDto?> CreateProductsAsync(ProductDto productDto);
        Task<ResponceDto?> UpdateProductsAsync(ProductDto productDto);
        Task<ResponceDto?> DeleteProductsAsync(int id);
    }
}
