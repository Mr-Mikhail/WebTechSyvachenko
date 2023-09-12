using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers.Api;

[Route(Routes.CategoryManagementSystem)]
public class CategoryManagementSystemApiController : ControllerBase
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryManagementSystemApiController(IRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    
    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllProductsAsync(CancellationToken token)
    {
        var products = await _categoryRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<CategoryModel>>(products);
        
        return Ok(viewModel);
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateProductAsync(CategoryModel category, CancellationToken token)
    {
        var data = _mapper.Map<Category>(category);
        await _categoryRepository.CreateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateProductAsync(CategoryModel category, CancellationToken token)
    {
        var data = _mapper.Map<Category>(category);
        await _categoryRepository.UpdateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteProductAsync(Guid id, CancellationToken token)
    {
        await _categoryRepository.DeleteAsync(id, token);

        return Ok();
    }
}