using AutoMapper;
using OnlineShop.Application.Models;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Application.Services;

public class DishService
{
    private readonly IRepository<Dish> _dishRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public DishService(IRepository<Dish> dishRepository,
        IRepository<Category> categoryRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<DishViewModel> CreateDishAsync(DishDataViewModel model, CancellationToken token)
    {
        var dish = _mapper.Map<Dish>(model.DishView);
        await _dishRepository.CreateAsync(dish, token);

        dish = await UpdateCategoriesForDish(model, dish, token);

        return _mapper.Map<DishViewModel>(dish);
    }
    
    public async Task UpdateDishAsync(DishDataViewModel model, CancellationToken token)
    {
        var dish = _mapper.Map<Dish>(model.DishView);
        await _dishRepository.UpdateAsync(dish, token);

        await UpdateCategoriesForDish(model, dish, token);
    }

    private async Task<Dish?> UpdateCategoriesForDish(DishDataViewModel model, Dish dish, CancellationToken token)
    {
        var categories =
            (await _categoryRepository.GetAsync(x => model.SelectedCategoryIds.Contains(x.Id), null, token)).ToList();
        dish = (await _dishRepository.GetAsync(x => x.Id == dish.Id, null, token)).FirstOrDefault();
        if (dish == null)
            return null;

        dish.Categories.Clear();
        dish.Categories.AddRange(categories);
        
        await _dishRepository.UpdateAsync(dish, token);
        return dish;
    }
}