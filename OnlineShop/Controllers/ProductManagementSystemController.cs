using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class ProductManagementSystemController : Controller
{
    private readonly IDatabaseService<Product> _productDatabaseService;
    private readonly IMapper _mapper;

    public ProductManagementSystemController(IDatabaseService<Product> productDatabaseService, IMapper mapper)
    {
        _productDatabaseService = productDatabaseService;
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

    public async Task<IActionResult> SaveAsync(ProductViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return RedirectToAction("Create");
        
        model.Id = Guid.NewGuid();

        var product = _mapper.Map<Product>(model);
        await _productDatabaseService.CreateAsync(product, token);
        
        return RedirectToAction("Created", model);
    }
    
    public IActionResult Created(ProductViewModel model)
    {
        return View(model);
    }

    public async Task<IActionResult> All(CancellationToken token)
    {
        var products = await _productDatabaseService.GetAllAsync(token);
        var viewModel = _mapper.Map<List<ProductViewModel>>(products);

        return View(viewModel);
    }
    
    public async Task<IActionResult> Edit(Guid id, CancellationToken token)
    {
        var products = await _productDatabaseService.GetAsync(x => x.Id == id, token);

        var product = products.FirstOrDefault();
        var viewModel = _mapper.Map<ProductViewModel>(product);
        
        return product == null ? View("All") : View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return View("Edit", model);
        
        var product = _mapper.Map<Product>(model);
        await _productDatabaseService.UpdateAsync(product, token);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _productDatabaseService.DeleteAsync(id, token);
        
        return RedirectToAction("Index");
    }
}