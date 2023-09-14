using AutoMapper;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;

namespace OnlineShop.Mappings;

public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishModel>();
        CreateMap<DishModel, Dish>();
        
        CreateMap<Category, CategoryModel>();
        CreateMap<CategoryModel, Category>();
    }
}