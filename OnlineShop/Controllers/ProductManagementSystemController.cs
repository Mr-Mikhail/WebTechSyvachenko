using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class ProductManagementSystemController : Controller
{
    private readonly IProductManagementService _productManagementService;
    private readonly IMapper _mapper;

    public ProductManagementSystemController(IProductManagementService productManagementService, IMapper mapper)
    {
        _productManagementService = productManagementService;
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
        var culture = CultureInfo.CurrentCulture;
        
        if (!ModelState.IsValid) 
            return RedirectToAction("Create");
        
        model.Id = Guid.NewGuid();

        var product = _mapper.Map<Product>(model);
        await _productManagementService.CreateProductAsync(product, token);
        
        return RedirectToAction("Created", model);
    }
    
    public IActionResult Created(ProductViewModel model)
    {
        return View(model);
    }

    public async Task<IActionResult> All(CancellationToken token)
    {
        var products = await _productManagementService.GetAllProductsAsync(token);
        var viewModel = _mapper.Map<List<ProductViewModel>>(products);

        return View(viewModel);
    }
    
    public IActionResult Edit(Guid id)
    {
        var productViewModel = new ProductViewModel
        {
            Id = id,
            ProductName = "Sample Product",
            ProductDescription = "Sample Description",
            ProductPrice = 100.00,
            Currency = "USD"
        };

        return View(productViewModel);
    }
    
    [HttpPost]
    public IActionResult Update(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {

            return RedirectToAction("All");
        }

        return View("Edit", model);
    }

    public IActionResult Delete(Guid id)
    {
        return RedirectToAction("Create");
    }
}