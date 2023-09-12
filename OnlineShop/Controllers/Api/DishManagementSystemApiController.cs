using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers.Api;

[Route(Routes.DishManagementSystem)]
public class DishManagementSystemApiController : ControllerBase
{
    private readonly IRepository<Dish> _productRepository;

    private readonly IMapper _mapper;

    public DishManagementSystemApiController(IRepository<Dish> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken token)
    {
        var products = await _productRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<DishViewModel>>(products);
        
        return Ok(viewModel);
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateProductAsync(DishViewModel dish, CancellationToken token)
    {
        var data = _mapper.Map<Dish>(dish);
        await _productRepository.CreateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateProductAsync(DishViewModel dish, CancellationToken token)
    {
        var data = _mapper.Map<Dish>(dish);
        await _productRepository.UpdateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteProductAsync(Guid id, CancellationToken token)
    {
        await _productRepository.DeleteAsync(id, token);

        return Ok();
    }
}