using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsApi.Models.Products;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using ProductsApi.Services.Products;

namespace ProductsApi.Controllers
{
    [EnableCors]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService userService)
        {
            _logger = logger;
            _productService = userService;
        }

        [HttpGet("api/product")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            var productDto = new ProductDto
            {
                Products = products,
                ActivePage = 1,
                MaxPages = 2
            };

            return Ok(productDto);
        }

        [HttpGet("api/product/{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }

            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("api/product")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Type) || product.Price == 0)
            {
                return BadRequest();
            }

            var createdProduct = await _productService.CreateProductAsync(product);

            return StatusCode(StatusCodes.Status201Created, createdProduct);
        }

        [HttpPut("api/product/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product product)
        {
            if (productId == 0 || product == null || productId != product.Id || string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Type) || product.Price == 0)
            {
                return BadRequest();
            }

            var existingProduct = await _productService.GetByIdAsync(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }

            var success = await _productService.UpdateAsync(productId, product);

            return success ? NoContent() : NotFound();
        }

        [HttpDelete("api/product/{productId}")]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }

            var product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var success = await _productService.DeleteAsync(productId);

            return success ? NoContent() : StatusCode(StatusCodes.Status410Gone);
        }
    }
}
