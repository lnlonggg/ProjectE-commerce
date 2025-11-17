using MyShop.Core.DTOs;
using System.Threading.Tasks;

namespace MyShop.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetProductsAsync(int page, int pageSize, string? search);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto productDto);
        Task<bool> UpdateProductAsync(int id, ProductCreateDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}