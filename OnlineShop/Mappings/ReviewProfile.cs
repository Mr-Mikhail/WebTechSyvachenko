using AutoMapper;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewViewModel>();
        CreateMap<ReviewViewModel, Review>();
    }
}