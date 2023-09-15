using AutoMapper;
using JetBrains.Annotations;
using OnlineShop.Controllers.Api.Category.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

[UsedImplicitly]
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryViewModel>();
        CreateMap<Category, CategoryApiResponse>();
        CreateMap<CategoryViewModel, Category>();
        CreateMap<CategoryApiRequest, Category>();
    }
}