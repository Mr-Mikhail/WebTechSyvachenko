using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;
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
            Categories = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.GetAllAsync(token))
        };

        return View(viewModel);
    }

    public async Task<IActionResult> SaveAsync(DishDataViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return RedirectToAction("Create");

        var categories =
            await _categoryRepository.GetAsync(x => model.SelectedCategoryIds.Contains(x.Id),
                FilteringOptions.AsNoTrackingInstance,
                token) as List<Category> ?? new List<Category>();
        var dish = _mapper.Map<Dish>(model.DishView);
        dish.Categories = categories;
        await _dishRepository.CreateAsync(dish, token);

        var viewModel = _mapper.Map<DishViewModel>(dish);
        
        return View("Created", viewModel);
    }

    public async Task<IActionResult> All(CancellationToken token)
    {
        var dishes = await _dishRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<DishViewModel>>(dishes);

        return View(viewModel);
    }
    
    public async Task<IActionResult> Edit(Guid id, CancellationToken token)
    {
        var dishes =
            await _dishRepository.GetAsync(x => x.Id == id, FilteringOptions.AsNoTrackingInstance, token);

        var dish = dishes.FirstOrDefault();
        if (dish == null)
            return View("Index");
        
        var model = _mapper.Map<DishViewModel>(dish);
        
        // TODO: Remove the code duplication
        var viewModel = new DishDataViewModel
        {
            DishView = model,
            Categories = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.GetAllAsync(token)),
            SelectedCategoryIds = model.Categories.Select(x => x.Id).ToList()
        };
        
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(DishDataViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return View("Edit", model);
        
        var dish = _mapper.Map<Dish>(model.DishView);
        await _dishRepository.UpdateAsync(dish, token);

        var categories =
            (await _categoryRepository.GetAsync(x => model.SelectedCategoryIds.Contains(x.Id), null, token)).ToList();
        dish = (await _dishRepository.GetAsync(x => x.Id == model.DishView.Id, null, token)).FirstOrDefault();
        if (dish == null)
            return Ok();

        dish.Categories.Clear();
        dish.Categories.AddRange(categories);
        
        await _dishRepository.UpdateAsync(dish, token);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _dishRepository.DeleteAsync(id, token);
        
        return RedirectToAction("Index");
    }
}