using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers.Api;

[Route(Routes.ProductManagementSystem)]
public class ProductManagementSystemApiController : ControllerBase
{
    private readonly IDatabaseService<Product> _productDatabaseService;

    private readonly IMapper _mapper;

    public ProductManagementSystemApiController(IDatabaseService<Product> productDatabaseService, IMapper mapper)
    {
        _productDatabaseService = productDatabaseService;
        _mapper = mapper;
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken token)
    {
        var products = await _productDatabaseService.GetAllAsync(token);
        var viewModel = _mapper.Map<List<ProductViewModel>>(products);
        
        return Ok(viewModel);
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateProductAsync(ProductViewModel product, CancellationToken token)
    {
        var data = _mapper.Map<Product>(product);
        await _productDatabaseService.CreateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateProductAsync(ProductViewModel product, CancellationToken token)
    {
        var data = _mapper.Map<Product>(product);
        await _productDatabaseService.UpdateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteProductAsync(Guid id, CancellationToken token)
    {
        await _productDatabaseService.DeleteAsync(id, token);

        return Ok();
    }
}