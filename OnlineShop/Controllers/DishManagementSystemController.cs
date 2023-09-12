using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class DishManagementSystemController : Controller
{
    private readonly IRepository<Dish> _productRepository;
    private readonly IMapper _mapper;

    public DishManagementSystemController(IRepository<Dish> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    // TODO: Add Db connection + services for business logics
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> SaveAsync(DishViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return RedirectToAction("Create");
        
        model.Id = Guid.NewGuid();

        var product = _mapper.Map<Dish>(model);
        await _productRepository.CreateAsync(product, token);
        
        return RedirectToAction("Created", model);
    }
    
    public IActionResult Created(DishViewModel model)
    {
        return View(model);
    }

    public async Task<IActionResult> All(CancellationToken token)
    {
        var products = await _productRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<DishViewModel>>(products);

        return View(viewModel);
    }
    
    public async Task<IActionResult> Edit(Guid id, CancellationToken token)
    {
        var products = await _productRepository.GetAsync(x => x.Id == id, token);

        var product = products.FirstOrDefault();
        var viewModel = _mapper.Map<DishViewModel>(product);
        
        return product == null ? View("All") : View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(DishViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return View("Edit", model);
        
        var product = _mapper.Map<Dish>(model);
        await _productRepository.UpdateAsync(product, token);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _productRepository.DeleteAsync(id, token);
        
        return RedirectToAction("Index");
    }
}