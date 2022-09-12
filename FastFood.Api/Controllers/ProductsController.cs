using FastFood.Domain.Dto;
using FastFood.Domain.Dto.Products.Request;
using FastFood.Domain.Dto.Products.Response;
using FastFood.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FastFood.Api.Controllers
{

    public class ProductsController : BaseController<ProductsController>
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductService _productService;
        public ProductsController(ILogger<ProductsController> logger, IProductService productService) : base(logger)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200">Returns the product created</response>
        /// <response code="404">If the product does not exist</response>
        /// <returns>the create product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseDto<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateAsync(ProductRequestDto dto)
        {
            _logger.LogInformation($"{nameof(CreateAsync)}: save new product ");
            var response = await _productService.CreateProduct(dto);
            _logger.LogInformation($"{nameof(CreateAsync)}: product saved");
            return Ok(response);
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">If the product does not exist</response>
        /// <returns>The request product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseDto<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetProductAsync(long id)
        {
            _logger.LogInformation($"{nameof(GetProductAsync)}: Get Product by Id: {id}");
            var response = await _productService.GetProduct(id);
            _logger.LogInformation($"{nameof(GetProductAsync)}: got product whith id: {id} ");
            return Ok(response);
        }

        /// <summary>
        /// Gets all the products by using the provided filter
        /// </summary>
        /// <response code="200">Returns a wrapper that contains a list of products</response>
        /// <response code="400">If some of the properties in the request are not valid</response>
        /// <returns>A wrapper that contains a list of all the products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiListResponseDto<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAllProducts()
        {
            Logger.LogInformation($"{nameof(GetAllProducts)}: Getting all products...");
            var response = await _productService.GetAllProducts();

            Logger.LogInformation($"{nameof(GetAllProducts)}: Got = {response.Result.Count} products");
            return Ok(response);
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">If the product does not exist</response>
        /// <returns>The request product</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponseDto<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status404NotFound)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateProduct(long id, ProductRequestDto dto)
        {
            Logger.LogInformation($"{nameof(UpdateProduct)}: update product with id: {id}");
            var response = await _productService.UpdateProduct(id,dto);

            Logger.LogInformation($"{nameof(UpdateProduct)}: Got product");
            return Ok(response);
        }

        /// <summary>
        /// delete product
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns the requested product</response>
        /// <response code="404">If the product does not exist</response>
        /// <returns>The request product</returns>
        [HttpDelete]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResponseDto), StatusCodes.Status400BadRequest)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            Logger.LogInformation($"{nameof(UpdateProduct)}: delete product with id: {id}");
            var response = await _productService.DeleteProduct(id);

            Logger.LogInformation($"{nameof(UpdateProduct)}: product has been deleted");
            return Ok(response);
        }


    }
}
