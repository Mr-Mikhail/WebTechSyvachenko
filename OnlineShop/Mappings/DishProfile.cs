using AutoMapper;
using JetBrains.Annotations;
using OnlineShop.Controllers.Api.Dish.Dto;
using OnlineShop.Controllers.Api.Photo.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

[UsedImplicitly]
public class DishProfile : Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishViewModel>()
            .ForMember(x => x.PhotoUrl,
                y => y.Ignore())
            .ForMember(x => x.PhotoName,
                y => 
                    y.MapFrom(z => z.Photo == null ? null : z.Photo.Name));
        CreateMap<Dish, DishApiResponse>();
        CreateMap<DishViewModel, Dish>()
            .ForMember(x => x.Photo,
                y => y.Ignore());
        CreateMap<DishApiRequest, Dish>();

        CreateMap<Photo, PhotoApiResponse>();
    }
}