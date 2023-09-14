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
    public async Task<IActionResult> GetAllDishesAsync(CancellationToken token)
    {
        try
        {
            var dishes = await _categoryRepository.GetAllAsync(token);
            var viewModel = _mapper.Map<List<CategoryModel>>(dishes);

            return Ok(viewModel);
        }
        catch
        {
            return NotFound("Failed to get all dishes.");
        }
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateDishAsync([FromBody] CategoryModel category, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid category.");

        if (category.DishId == Guid.Empty)
            return BadRequest("Invalid id");

        try
        {
            var data = _mapper.Map<Category>(category);
            await _categoryRepository.CreateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to create dish.");
        }
    }

    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateDishAsync([FromBody] CategoryModel category, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid category.");

        if (category.Id == Guid.Empty || category.DishId == Guid.Empty)
            return BadRequest("Invalid id");

        try
        {
            var data = _mapper.Map<Category>(category);
            await _categoryRepository.UpdateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to update dish.");
        }
    }

    [HttpDelete(Routes.Delete)]
    public async Task<IActionResult> DeleteDishAsync([FromQuery] Guid id, CancellationToken token)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid id");

        try
        {

            await _categoryRepository.DeleteAsync(id, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to delete the dish.");
        }
    }
}