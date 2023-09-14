using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers;

public class DishController : Controller
{
    private readonly IRepository<Dish> _dishRepository;
    private readonly IRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;
    
    public DishController(IRepository<Dish> dishRepository, IMapper mapper, IRepository<Review> reviewRepository)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
    }

    public async Task<IActionResult> Index(CancellationToken token)
    {
        var dishes = await _dishRepository.GetAllAsync(token);
        var viewModels = _mapper.Map<List<DishModel>>(dishes);
        return View(viewModels);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken token)
    {
        var dish = (await _dishRepository.GetAsync(x => x.Id == id, token)).FirstOrDefault();
        if (dish == null)
            RedirectToAction("Index");

        var viewModel = _mapper.Map<DishModel>(dish);
        
        return View(viewModel);
    }

    public async Task<IActionResult> CreateReview(ReviewModel reviewModel, CancellationToken token)
    {
        var review = _mapper.Map<Review>(reviewModel);
        await _reviewRepository.CreateAsync(review, token);
        
        return RedirectToAction("Details", new { id = reviewModel.DishId });
    }
}