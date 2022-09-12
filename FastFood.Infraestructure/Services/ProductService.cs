using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Products.Request;
using FastFood.Domain.Dto.Products.Response;
using FastFood.Domain.Entities;
using FastFood.Domain.Enums;
using FastFood.Domain.Exceptions;
using FastFood.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FastFood.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IAppDataService _appDataService;
        public ProductService(ILogger<ProductService> logger, IAppDataService appDataService)
        {
            _logger = logger;
            _appDataService = appDataService;
        }
        public async Task<ApiResponseDto<ProductResponseDto>> CreateProduct(ProductRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateProduct)}: validating existent product");

            var existProduct = await _appDataService.Products.ExistsAsync(r=>
               (r.Name == dto.Name || r.ProductCode == dto.ProductCode) && 
                r.Status != StatusType.Deleted);
            if(existProduct)
            {
                var message = $"{nameof(GetProduct)}: produc whith name: {dto.Name} or code: {dto.ProductCode} exist";
                _logger.LogInformation(message);
                throw new InvalidRequestException(message,AppMessageType.ApiProductExistent);
            }


            _logger.LogInformation($"{nameof(CreateProduct)}: triying to add new product");
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                ProductCode = dto.ProductCode,
                Stock = dto.Stock,
                Status = StatusType.Activated
            };

             _appDataService.Products.Add(product);
             await _appDataService.SaveChangesAsync();

            _logger.LogInformation($"{nameof(CreateProduct)}: product has seved");

            return await GetProduct(product.Id);
        }

        public async Task<EmptyResponseDto> DeleteProduct(long id)
        {
            _logger.LogInformation($"{nameof(DeleteProduct)}: validating product with id: {id}");
            var result = await _appDataService.Products.FirstOrDefaultAsync(r => r.Id == id && r.Status != StatusType.Deleted);

            if (result == null)
            {
                var message = $"{nameof(DeleteProduct)}: product whith: {id} not found";
                _logger.LogInformation(message);
                throw new NotFoundException(message);
            }

            result.Status = StatusType.Deleted;
            _logger.LogInformation($"{nameof(DeleteProduct)}: Deleting product whit id : {id}");
            _appDataService.Products.Update(result);
            await _appDataService.SaveChangesAsync();

            _logger.LogInformation($"{nameof(DeleteProduct)}: product whit id: {id} has been deleted");
            return new EmptyResponseDto(true);
        }

        public async Task<ApiListResponseDto<ProductResponseDto>> GetAllProducts()
        {
            var response = new ApiListResponseDto<ProductResponseDto>();
            _logger.LogInformation($"{nameof(GetAllProducts)}: Getting all products");
            var result = await _appDataService.Products.GetAllAsync(r=> r.Status != StatusType.Deleted);

            _logger.LogInformation($"{nameof(GetAllProducts)}: got all products count {result.Count()}");
            response.Result = result.Select(s => new ProductResponseDto
            {
                CreatedAt = s.CreatedAt,
                Description = s.Description,
                Id = s.Id,
                Name = s.Name,
                ProductCode = s.ProductCode,
                Stock = s.Stock,
                UpdatedAt = s.UpdatedAt
            }).ToList();
            response.Succeed = true;

            return response;
        }

        public async Task<ApiResponseDto<ProductResponseDto>> GetProduct(long id)
        {
            _logger.LogInformation($"{nameof(GetProduct)}: Getting product with id: {id}");
            var result = await _appDataService.Products.FirstOrDefaultAsync(r => r.Id == id && r.Status != StatusType.Deleted);

            if (result == null)
            {
                var message = $"{nameof(GetProduct)}: produc whith: {id} not found";
                _logger.LogInformation(message);
                throw new NotFoundException(message);
            }

            return new ApiResponseDto<ProductResponseDto>(new ProductResponseDto {
               Name = result.Name,
               CreatedAt = result.CreatedAt,
               Description = result.Description,
               Id= result.Id,
               ProductCode = result.ProductCode,
               Stock = result.Stock,
               UpdatedAt = result.UpdatedAt
            });
        }

        public async Task<ApiResponseDto<ProductResponseDto>> UpdateProduct(long id, ProductRequestDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateProduct)}: validating product with id: {id}");
            var result = await _appDataService.Products.FirstOrDefaultAsync(r => r.Id == id);

            if (result == null)
            {
                var message = $"{nameof(UpdateProduct)}: product whith: {id} not found";
                _logger.LogInformation(message);
                throw new NotFoundException(message);
            }

            if (result.Name != dto.Name)
            {
                var existProduct = await _appDataService.Products.ExistsAsync(r => r.Name == dto.Name && r.Status != StatusType.Deleted);
                if (existProduct)
                {
                    var message = $"{nameof(UpdateProduct)}: produc whith name: {dto.Name} exist";
                    _logger.LogInformation(message);
                    throw new InvalidRequestException(message, AppMessageType.ApiProductExistent);
                }
            }

            if (result.ProductCode != dto.ProductCode)
            {
                var existProduct = await _appDataService.Products.ExistsAsync(r =>  r.ProductCode == dto.ProductCode && r.Status != StatusType.Deleted);
                if (existProduct)
                {
                    var message = $"{nameof(UpdateProduct)}: produc whith code: {dto.ProductCode} exist";
                    _logger.LogInformation(message);
                    throw new InvalidRequestException(message, AppMessageType.ApiProductExistent);
                }
            }

            result.ProductCode = dto.ProductCode;
            result.Stock = dto.Stock;
            result.Description = dto.Description;
            result.Name = dto.Name;

            _appDataService.Products.Update(result);
            await _appDataService.SaveChangesAsync();
            _logger.LogInformation($"{nameof(UpdateProduct)}: product with id: {id} has been updated");
            return await GetProduct(result.Id);
        }
    }
}
