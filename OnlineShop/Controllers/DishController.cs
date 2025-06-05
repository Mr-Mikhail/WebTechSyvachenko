using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Models;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class DishController : Controller
{
    private readonly IRepository<Dish> _dishRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly DishService _dishService;
    private readonly IMapper _mapper;
    
    public DishController(IRepository<Dish> dishRepository, IMapper mapper, IRepository<Review> reviewRepository, DishService dishService)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
        _dishService = dishService;
    }

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var viewModel = await _dishService.GetAllDishes(token);
        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken token)
    {
        var dish = (await _dishRepository.GetAsync(x => x.Id == id, FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();
        if (dish == null)
        {
            return RedirectToAction("Index");
        }

        var viewModel = _mapper.Map<DishViewModel>(dish);
        
        return View(viewModel);
    }

    public async Task<IActionResult> CreateReview(ReviewViewModel reviewViewModel, CancellationToken token)
    {
        var review = _mapper.Map<Review>(reviewViewModel);
        await _reviewRepository.CreateAsync(review, token);
        
        return RedirectToAction("Details", new { id = reviewViewModel.DishId });
    }
}