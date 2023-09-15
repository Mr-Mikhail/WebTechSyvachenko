using AutoMapper;
using JetBrains.Annotations;
using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Controllers.Api.Dish.Dto;
using OnlineShop.Controllers.Api.Review.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

[UsedImplicitly]
public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishViewModel>();
        CreateMap<Dish, DishApiResponse>();
        CreateMap<DishViewModel, Dish>();
        CreateMap<DishApiRequest, Dish>();

        CreateMap<PhotoApiRequest, Photo>();
    }
}