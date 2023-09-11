using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class ProductsController : Controller
{
    private readonly IDatabaseService<Product> _productDatabaseService;
    private readonly IDatabaseService<Review> _reviewDatabaseService;
    private readonly IMapper _mapper;
    
    public ProductsController(IDatabaseService<Product> productDatabaseService, IMapper mapper, IDatabaseService<Review> reviewDatabaseService)
    {
        _productDatabaseService = productDatabaseService;
        _mapper = mapper;
        _reviewDatabaseService = reviewDatabaseService;
    }

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var products = await _productDatabaseService.GetAllAsync(token);
        var viewModels = _mapper.Map<List<ProductViewModel>>(products);
        return View(viewModels);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken token)
    {
        var product = (await _productDatabaseService.GetAsync(x => x.Id == id, token)).FirstOrDefault();
        if (product == null)
            RedirectToAction("Index");

        var viewModel = _mapper.Map<ProductViewModel>(product);
        
        return View(viewModel);
    }

    public async Task<IActionResult> CreateReview(ReviewViewModel reviewViewModel, CancellationToken token)
    {
        var review = _mapper.Map<Review>(reviewViewModel);
        await _reviewDatabaseService.CreateAsync(review, token);
        
        return RedirectToAction("Details", new { id = reviewViewModel.ProductId });
    }
}