using AutoMapper;
using OnlineShop.Controllers.Api.Review.Dto;
using OnlineShop.Domain.Models;

namespace OnlineShop.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewModel>();
        CreateMap<Review, ReviewApiResponse>();
        CreateMap<ReviewModel, Review>();
        CreateMap<ReviewApiRequest, Review>();
    }
}