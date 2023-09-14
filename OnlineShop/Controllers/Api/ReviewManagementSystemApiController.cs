using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

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
        try
        {
            var reviews = await _reviewRepository.GetAllAsync(token);
            var viewModel = _mapper.Map<List<ReviewModel>>(reviews);

            return Ok(viewModel);
        }
        catch
        {
            return NotFound("Failed to get all reviews");
        }
    }

    [HttpPost(Routes.Create)]
    public async Task<IActionResult> CreateReviewAsync([FromBody] ReviewModel review, CancellationToken token)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid review format");

            var data = _mapper.Map<Review>(review);
            await _reviewRepository.CreateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to create review");
        }
    }

    [HttpPost(Routes.Update)]
    public async Task<IActionResult> UpdateReviewAsync([FromBody] ReviewModel review, CancellationToken token)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid review format");

            if (review.Id == Guid.Empty)
                return BadRequest("Id for review was not specified");

            var data = _mapper.Map<Review>(review);
            await _reviewRepository.UpdateAsync(data, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to update review.");
        }
    }

    [HttpPost(Routes.Delete)]
    public async Task<IActionResult> DeleteReviewAsync([FromQuery] Guid id, CancellationToken token)
    {
        try
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid Id");

            await _reviewRepository.DeleteAsync(id, token);

            return Ok();
        }
        catch
        {
            return BadRequest("Failed to delete review.");
        }
    }
}