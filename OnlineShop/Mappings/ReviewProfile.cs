using AutoMapper;
using JetBrains.Annotations;
using OnlineShop.Controllers.Api.Review.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

[UsedImplicitly]
public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewViewModel>();
        CreateMap<Review, ReviewApiResponse>();
        CreateMap<ReviewViewModel, Review>();
        CreateMap<ReviewApiRequest, Review>();
    }
}