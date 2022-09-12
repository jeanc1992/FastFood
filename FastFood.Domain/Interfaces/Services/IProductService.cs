using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Products.Request;
using FastFood.Domain.Dto.Products.Response;

namespace FastFood.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<ApiResponseDto<ProductResponseDto>> GetProduct(long id);
        Task<ApiListResponseDto<ProductResponseDto>> GetAllProducts();
        Task<ApiResponseDto<ProductResponseDto>> CreateProduct(ProductRequestDto dto);
        Task<EmptyResponseDto> DeleteProduct(long id);
        Task<ApiResponseDto<ProductResponseDto>> UpdateProduct(long id, ProductRequestDto dto);

    }
}
