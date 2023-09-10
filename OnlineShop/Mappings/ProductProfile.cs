using AutoMapper;
using OnlineShop.Domain.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductViewModel>();
        CreateMap<ProductViewModel, Product>();
    }
}