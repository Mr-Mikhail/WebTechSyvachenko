using AutoMapper;
using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Controllers.Api.Dish.Dto;
using OnlineShop.Controllers.Api.Review.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishModel>();
        CreateMap<Dish, DishApiResponse>();
        CreateMap<DishModel, Dish>();
        CreateMap<DishApiRequest, Dish>();

        CreateMap<PhotoApiRequest, Photo>();
        
        CreateMap<Category, CategoryViewModel>();
        CreateMap<Category, CategoryApiResponse>();
        CreateMap<CategoryViewModel, Category>();
        CreateMap<CategoryApiRequest, Category>();
    }
}