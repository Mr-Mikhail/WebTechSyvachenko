using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers.Api;

[Route(Routes.DishManagementSystem)]
public class DishManagementSystemApiController : ControllerBase
{
    private readonly IRepository<Dish> _dishRepository;

    private readonly IMapper _mapper;

    public DishManagementSystemApiController(IRepository<Dish> dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllDishesAsync(CancellationToken token)
    {
        try
        {
            var dishes = await _dishRepository.GetAllAsync(token);
            var viewModel = _mapper.Map<List<DishModel>>(dishes);

            return Ok(viewModel);
        }
        catch
        {
            return NotFound("Failed to get all dishes");
        }
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateDishAsync([FromBody] DishModel dish, CancellationToken token)
    {
        try
        {
            var data = _mapper.Map<Dish>(dish);
            await _dishRepository.CreateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to create a dish");
        }
    }

    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateDishAsync([FromBody] DishModel dish, CancellationToken token)
    {
        try
        {
            var data = _mapper.Map<Dish>(dish);
            await _dishRepository.UpdateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to update dish");
        }
    }

    [HttpPost(Routes.Delete)]
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