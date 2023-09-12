using AutoMapper;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Dish, DishViewModel>();
        CreateMap<DishViewModel, Dish>();
        
        CreateMap<Category, CategoryViewModel>();
        CreateMap<CategoryViewModel, Category>();
    }
}