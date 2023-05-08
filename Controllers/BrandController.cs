using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsApi.Models.Brands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using ProductsApi.Services.Brands;
using System.Threading;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    [EnableCors]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;

        public BrandController(ILogger<BrandController> logger, IBrandService userService)
        {
            _logger = logger;
            _brandService = userService;
        }

        [HttpGet("api/brand")]
        public async Task<IActionResult> GetAll([FromQuery] UrlQueryParameters urlQueryParameters,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var brands = await _brandService.GetByPagedAsync(urlQueryParameters.Limit,
                    urlQueryParameters.Page, urlQueryParameters.SortOrder, urlQueryParameters.sortAsc, cancellationToken);
                return Ok(brands);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("api/brand/{brandId}")]
        public async Task<IActionResult> GetById(int brandId)
        {
            if (brandId <= 0)
            {
                return BadRequest();
            }

            var brand = await _brandService.GetByIdAsync(brandId);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        [HttpPost("api/brand")]
        public async Task<IActionResult> CreateBrand([FromBody] Brand brand)
        {
            if (brand == null || string.IsNullOrEmpty(brand.Name))
            {
                return BadRequest();
            }

            var createdBrand = await _brandService.CreateBrandAsync(brand);

            return StatusCode(StatusCodes.Status201Created, createdBrand);
        }

        [HttpPut("api/brand/{brandId}")]
        public async Task<IActionResult> UpdateBrand(int brandId, [FromBody] Brand brand)
        {
            if (brandId == 0 || brand == null || brandId != brand.Id || string.IsNullOrEmpty(brand.Name))
            {
                return BadRequest();
            }

            var existingBrand = await _brandService.GetByIdAsync(brandId);
            if (existingBrand == null)
            {
                return NotFound();
            }

            var success = await _brandService.UpdateAsync(brandId, brand);

            return success ? NoContent() : NotFound();
        }

        [HttpDelete("api/brand/{brandId}")]
        public async Task<IActionResult> Delete([FromRoute] int brandId)
        {
            if (brandId <= 0)
            {
                return BadRequest();
            }

            var brand = await _brandService.GetByIdAsync(brandId);
            if (brand == null)
            {
                return NotFound();
            }

            var success = await _brandService.DeleteAsync(brandId);

            return success ? NoContent() : StatusCode(StatusCodes.Status410Gone);
        }

        [HttpGet("api/brand/version")]
        public IActionResult GetVersion()
        {
            return Ok("V1.0.0");
        }
    }
}
