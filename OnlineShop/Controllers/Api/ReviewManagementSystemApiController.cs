using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers.Api;

[Route(Routes.ReviewManagementSystem)]
public class ReviewManagementSystemApiController : ControllerBase
{
    private readonly IRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewManagementSystemApiController(IRepository<Review> reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }
    
    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllReviewsAsync(CancellationToken token)
    {
        var reviews = await _reviewRepository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<ReviewViewModel>>(reviews);
        
        return Ok(viewModel);
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateReviewAsync(ReviewViewModel review, CancellationToken token)
    {
        var data = _mapper.Map<Review>(review);
        await _reviewRepository.CreateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateReviewAsync(ReviewViewModel review, CancellationToken token)
    {
        var data = _mapper.Map<Review>(review);
        await _reviewRepository.UpdateAsync(data, token);
        
        return Ok();
    }
    
    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteReviewAsync(Guid id, CancellationToken token)
    {
        await _reviewRepository.DeleteAsync(id, token);

        return Ok();
    }

}