using AutoMapper;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;

namespace OnlineShop.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewModel>();
        CreateMap<ReviewModel, Review>();
    }
}