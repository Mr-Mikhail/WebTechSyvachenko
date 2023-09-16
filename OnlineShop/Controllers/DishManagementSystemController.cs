using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class DishManagementSystemController : Controller
{
    private readonly IRepository<Dish> _dishRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly FileService _fileService;
    private readonly DishService _dishService;
    private readonly IMapper _mapper;

    public DishManagementSystemController(IRepository<Dish> dishRepository, IMapper mapper, IRepository<Category> categoryRepository, FileService fileService, DishService dishService)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _fileService = fileService;
        _dishService = dishService;
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
        return !ModelState.IsValid
            ? View("Edit", model)
            : View("Created", await _dishService.CreateDishAsync(model, token));
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

        await _dishService.UpdateDishAsync(model, token);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _dishRepository.DeleteAsync(id, token);
        
        return RedirectToAction("Index");
    }
}