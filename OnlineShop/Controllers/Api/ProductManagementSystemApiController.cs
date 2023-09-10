using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers.Api;

[Route(Routes.ProductManagementSystem)]
public class ProductManagementSystemApiController : ControllerBase
{
    // TODO: Add the db + services + update this
    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        return Ok("123");
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateProductAsync()
    {
        return Ok("123");
    }
    
    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateProductAsync()
    {
        return Ok("123");
    }
    
    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteProductAsync(Guid id)
    {
        return Ok("123");
    }
}