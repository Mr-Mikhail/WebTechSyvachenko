using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class DishManagementSystemController : Controller
{
    private readonly IRepository<Dish> _dishRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public DishManagementSystemController(IRepository<Dish> dishRepository, IMapper mapper, IRepository<Category> categoryRepository)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Create(CancellationToken token)
    {
        var viewModel = new DishDataViewModel
        {
            Categories = _mapper.Map<List<CategoryModel>>(await _categoryRepository.GetAllAsync(token))
        };

        return View(viewModel);
    }

    public async Task<IActionResult> SaveAsync(DishDataViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return RedirectToAction("Create");

        var categories =
            await _categoryRepository.GetAsync(x => model.SelectedCategoryIds.Contains(x.Id),
                token) as List<Category> ?? new List<Category>();
        
        model.Dish.Id = Guid.NewGuid();
        var dish = _mapper.Map<Dish>(model.Dish);

        model.Categories = _mapper.Map<List<CategoryModel>>(categories);
        dish.Categories = categories;
        
        await _dishRepository.CreateAsync(dish, token);

        return View("Created", model);
    }

    public async Task<IActionResult> All(CancellationToken token)
    {
        var dishes = await _dishRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<DishModel>>(dishes);

        return View(viewModel);
    }
    
    public async Task<IActionResult> Edit(Guid id, CancellationToken token)
    {
        var dishes = await _dishRepository.GetAsync(x => x.Id == id, token);

        var dish = dishes.FirstOrDefault();
        if (dish == null)
            return View("Index");
        
        var viewModel = _mapper.Map<DishModel>(dish);
        
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(DishModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return View("Edit", model);
        
        var dish = _mapper.Map<Dish>(model);
        await _dishRepository.UpdateAsync(dish, token);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _dishRepository.DeleteAsync(id, token);
        
        return RedirectToAction("Index");
    }
}