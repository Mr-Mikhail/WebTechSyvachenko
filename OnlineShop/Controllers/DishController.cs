using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers;

public class DishController : Controller
{
    private readonly IRepository<Dish> _productRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;
    
    public DishController(IRepository<Dish> productRepository, IMapper mapper, IRepository<Review> reviewRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
    }

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var products = await _productRepository.GetAllAsync(token);
        var viewModels = _mapper.Map<List<DishModel>>(products);
        return View(viewModels);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken token)
    {
        var product = (await _productRepository.GetAsync(x => x.Id == id, token)).FirstOrDefault();
        if (product == null)
            RedirectToAction("Index");

        var viewModel = _mapper.Map<DishModel>(product);
        
        return View(viewModel);
    }

    public async Task<IActionResult> CreateReview(ReviewModel reviewModel, CancellationToken token)
    {
        var review = _mapper.Map<Review>(reviewModel);
        await _reviewRepository.CreateAsync(review, token);
        
        return RedirectToAction("Details", new { id = reviewModel.ProductId });
    }
}