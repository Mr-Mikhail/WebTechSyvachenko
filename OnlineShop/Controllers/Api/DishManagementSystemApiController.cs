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
        var dishes = await _dishRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<DishModel>>(dishes);
        
        return Ok(viewModel);
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateDishAsync(DishModel dish, CancellationToken token)
    {
        var data = _mapper.Map<Dish>(dish);
        await _dishRepository.CreateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateDishAsync(DishModel dish, CancellationToken token)
    {
        var data = _mapper.Map<Dish>(dish);
        await _dishRepository.UpdateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteDishAsync(Guid id, CancellationToken token)
    {
        await _dishRepository.DeleteAsync(id, token);

        return Ok();
    }
}