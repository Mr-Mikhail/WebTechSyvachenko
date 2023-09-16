using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineShop.Application.Models;
using OnlineShop.Controllers.Api.Dish.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers.Api.Dish;

[Route(Routes.DishManagementSystem)]
public class DishManagementSystemApiController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly IRepository<Domain.Models.Dish> _dishRepository;
    private readonly IMapper _mapper;

    public DishManagementSystemApiController(IRepository<Domain.Models.Dish> dishRepository, IMapper mapper, IMemoryCache memoryCache)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllDishesAsync(CancellationToken token)
    {
        try
        {
            var data = await _memoryCache.GetOrCreateAsync<List<DishApiResponse>>("all_dishes",
                async entry =>
                {

                    var dishes = await _dishRepository.GetAllAsync(token);
                    var response = _mapper.Map<List<DishApiResponse>>(dishes);

                    entry.Value = response;
                    entry.SlidingExpiration = TimeSpan.FromMinutes(10);

                    return response;
                });
            return Ok(data);
        }
        catch
        {
            return NotFound("Failed to get all dishes");
        }
    }
    
    [HttpGet(Routes.Filtered)]
    public async Task<IActionResult> GetFilteredDishesAsync([FromBody] PaginationModel model, CancellationToken token)
    {
        try
        {
            var dishes = await _dishRepository.GetAsync(_ => true, new FilteringOptions(model), token);
            var response = _mapper.Map<List<DishApiResponse>>(dishes);

            return Ok(response);
        }
        catch
        {
            return NotFound("Failed to get all dishes");
        }
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateDishAsync([FromBody] DishApiRequest dish, CancellationToken token)
    {
        try
        {
            var data = _mapper.Map<Domain.Models.Dish>(dish);
            await _dishRepository.CreateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to create a dish");
        }
    }

    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateDishAsync([FromBody] DishApiRequest dish, CancellationToken token)
    {
        if (dish.Id == Guid.Empty)
            return BadRequest("Dish not specified");
        
        try
        {
            var data = _mapper.Map<Domain.Models.Dish>(dish);
            await _dishRepository.UpdateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to update dish");
        }
    }

    [HttpDelete(Routes.Delete)]
    public async Task<IActionResult> DeleteDishAsync([FromQuery] Guid id, CancellationToken token)
    {
        try
        {
            if (id == Guid.Empty)
                return BadRequest("Incorrect Id specified");
            
            await _dishRepository.DeleteAsync(id, token);
            return Ok();
        }
        catch
        {
            return BadRequest("Failed to delete dish");
        }
    }
}