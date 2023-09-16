using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;
using OnlineShop.Controllers.Api.DishCategory.Dto;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers.Api.DishCategory;

[Route(Routes.DishCategoryManagementSystem)]
public class DishCategoryManagementSystemApiController : ControllerBase
{
    private readonly IRepository<Domain.Models.Dish> _dishRepository;
    private readonly IRepository<Domain.Models.Category> _categoryRepository;

    public DishCategoryManagementSystemApiController(IRepository<Domain.Models.Dish> dishRepository, IRepository<Domain.Models.Category> categoryRepository)
    {
        _dishRepository = dishRepository;
        _categoryRepository = categoryRepository;
    }
    
    [HttpPost($"{Routes.Categories}/{Routes.Add}")]
    public async Task<IActionResult> AddDishCategories([FromBody] CategoriesForDishRequest model, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var dish = (await _dishRepository.GetAsync(x => x.Id == model.DishId, FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();
        var categories = (await _categoryRepository.GetAsync(x => model.CategoryIds.Contains(x.Id), FilteringOptions.AsNoTrackingInstance, token)).ToList();

        if (dish == null)
            return BadRequest("Wrong id");
        
        var newCategories = categories.Where(x => dish.Categories.All(y => y.Id != x.Id));

        dish.Categories.AddRange(newCategories);
        await _dishRepository.UpdateAsync(dish, token);

        return Ok();
    }
    
    [HttpPost($"{Routes.Dishes}/{Routes.Add}")]
    public async Task<IActionResult> AddCategoryDishes([FromBody] DishesForCategoryRequest model, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var category = (await _categoryRepository.GetAsync(x => x.Id == model.CategoryId, FilteringOptions.AsNoTrackingInstance, token))
            .FirstOrDefault();
        var dishes = (await _dishRepository.GetAsync(x => model.DishIds.Contains(x.Id), FilteringOptions.AsNoTrackingInstance, token)).ToList();
        
        if (category == null)
            return BadRequest("Wrong id");
        
        var newDishes = dishes.Where(x => category.Dishes.All(y => y.Id != x.Id));
        category.Dishes.AddRange(newDishes);
        
        await _categoryRepository.UpdateAsync(category, token);

        return Ok();
    }

    [HttpPost($"{Routes.Categories}/{Routes.Replace}")]
    public async Task<IActionResult> ReplaceDishCategories([FromBody] CategoriesForDishRequest model, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var dish = (await _dishRepository.GetAsync(x => x.Id == model.DishId, FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();
        var categories = await _categoryRepository.GetAsync(x => model.CategoryIds.Contains(x.Id), FilteringOptions.AsNoTrackingInstance, token);

        if (dish == null)
            return BadRequest("Wrong id");

        dish.Categories.Clear();
        dish.Categories.AddRange(categories);
        await _dishRepository.UpdateAsync(dish, token);

        return Ok();
    }
    
    [HttpDelete($"{Routes.Categories}/{Routes.Delete}")]
    public async Task<IActionResult> DeleteDishCategories([FromQuery] Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var dish = (await _dishRepository.GetAsync(x => x.Id == id, FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();

        if (dish == null)
            return BadRequest("Wrong id");

        dish.Categories.Clear();
        await _dishRepository.UpdateAsync(dish, token);

        return Ok();
    }
    
    [HttpPost($"{Routes.Dishes}/{Routes.Replace}")]
    public async Task<IActionResult> ReplaceCategoryDishes([FromBody] DishesForCategoryRequest model, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var category = (await _categoryRepository.GetAsync(x => x.Id == model.CategoryId, FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();
        var dishes = await _dishRepository.GetAsync(x => model.DishIds.Contains(x.Id), FilteringOptions.AsNoTrackingInstance, token);

        if (category == null)
            return BadRequest("Wrong id");

        category.Dishes.Clear();
        category.Dishes.AddRange(dishes);
        await _categoryRepository.UpdateAsync(category, token);

        return Ok();
    }
    
    [HttpDelete($"{Routes.Dishes}/{Routes.Delete}")]
    public async Task<IActionResult> DeleteCategoryDishes([FromQuery] Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var category = (await _categoryRepository.GetAsync(x => x.Id == id, FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();

        if (category == null)
            return BadRequest("Wrong id");

        category.Dishes.Clear();
        await _categoryRepository.UpdateAsync(category, token);

        return Ok();
    }
}