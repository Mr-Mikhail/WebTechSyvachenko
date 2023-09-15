using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;
using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers.Api.Category;

[Route(Routes.CategoryManagementSystem)]
public class CategoryManagementSystemApiController : ControllerBase
{
    private readonly IRepository<Domain.Models.Category> _categoryRepository;
    private readonly IRepository<Domain.Models.Dish> _dishRepository;
    private readonly IMapper _mapper;

    public CategoryManagementSystemApiController(IRepository<Domain.Models.Category> categoryRepository, IMapper mapper, IRepository<Domain.Models.Dish> dishRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _dishRepository = dishRepository;
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken token)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync(token);
            var response = _mapper.Map<List<CategoryApiResponse>>(categories);

            return Ok(response);
        }
        catch
        {
            return NotFound("Failed to get all categories.");
        }
    }

    [HttpGet(Routes.Filtered)]
    public async Task<IActionResult> GetFilteredCategoriesAsync([FromBody] PaginationModel model, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid request");
        try
        {
            var categories = await _categoryRepository.GetAsync(_ => true, new FilteringOptions(model), token);
            var response = _mapper.Map<List<CategoryApiResponse>>(categories);

            return Ok(response);
        }
        catch
        {
            return NotFound("Failed to get all categories.");
        }
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryApiRequest request, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid category.");
        
        try
        {
            var data = _mapper.Map<Domain.Models.Category>(request);
            await _categoryRepository.CreateAsync(data, token);
            
            return Ok();
        }
        catch
        {
            return BadRequest("Failed to create category.");
        }
    }

    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryApiRequest request, CancellationToken token)
    {
        if (!ModelState.IsValid || request.Id == Guid.Empty)
            return BadRequest("Invalid category.");

        try
        {
            var data = _mapper.Map<Domain.Models.Category>(request);
            await _categoryRepository.UpdateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to update category.");
        }
    }

    [HttpDelete(Routes.Delete)]
    public async Task<IActionResult> DeleteCategoryAsync([FromQuery] Guid id, CancellationToken token)
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
            return BadRequest("Failed to delete the category.");
        }
    }
}