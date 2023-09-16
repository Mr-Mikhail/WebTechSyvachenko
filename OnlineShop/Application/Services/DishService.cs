using System.Globalization;
using AutoMapper;
using OnlineShop.Application.Models;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;
using OnlineShop.ViewModels;

namespace OnlineShop.Application.Services;

public class DishService
{
    private readonly IRepository<Dish> _dishRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Photo> _photoRepository;
    private readonly FileService _fileService;
    private readonly IMapper _mapper;

    public DishService(IRepository<Dish> dishRepository,
        IRepository<Category> categoryRepository, IMapper mapper, IRepository<Photo> photoRepository, FileService fileService)
    {
        _dishRepository = dishRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _photoRepository = photoRepository;
        _fileService = fileService;
    }

    public async Task<List<DishViewModel>> GetAllDishes(CancellationToken token)
    {
        var dishes = await _dishRepository.GetAllAsync(token);
        var viewModels = _mapper.Map<List<DishViewModel>>(dishes);

        var updatedViewModels = await Task.WhenAll(viewModels.Select(x => UpdateViewModelWithImage(x, token)));

        return updatedViewModels.ToList();
    }

    private async Task<DishViewModel> UpdateViewModelWithImage(DishViewModel model, CancellationToken token)
    {
        if (model.PhotoName == null) 
            return model;
            
        var image = await _fileService.GetBlobByName(model.PhotoName, token);
            
        if (image is { Uri: not null })
        {
            model.PhotoUrl = image.Uri;
        }

        return model;
    }
    
    public async Task<DishViewModel> CreateDishAsync(DishDataViewModel model, CancellationToken token)
    {
        var dish = _mapper.Map<Dish>(model.DishView);
        await _dishRepository.CreateAsync(dish, token);

        dish = await UpdateCategoriesForDishAsync(model, dish, token);
        dish = await UpdatePhotoForDishAsync(model, dish, token);

        return _mapper.Map<DishViewModel>(dish);
    }
    
    public async Task UpdateDishAsync(DishDataViewModel model, CancellationToken token)
    {
        var dish = _mapper.Map<Dish>(model.DishView);
        await _dishRepository.UpdateAsync(dish, token);

        await UpdateCategoriesForDishAsync(model, dish, token);
        await UpdatePhotoForDishAsync(model, dish, token);
    }

    private async Task<Dish?> UpdateCategoriesForDishAsync(DishDataViewModel model, Dish dish, CancellationToken token)
    {
        var categories =
            (await _categoryRepository.GetAsync(x => model.SelectedCategoryIds.Contains(x.Id), null, token)).ToList();
        dish = (await _dishRepository.GetAsync(x => x.Id == dish.Id, null, token)).FirstOrDefault();
        if (dish == null)
            return null;

        dish.Categories.Clear();
        dish.Categories.AddRange(categories);
        
        await _dishRepository.UpdateAsync(dish, token);
        return dish;
    }

    private async Task<Dish?> UpdatePhotoForDishAsync(DishDataViewModel model, Dish? dish, CancellationToken token)
    {
        if (dish == null)
            return null;

        if (model.Photo == null)
            return dish;

        var photo = (await _photoRepository.GetAsync(x => x.Name == model.Photo.FileName,
            FilteringOptions.AsNoTrackingInstance, token)).FirstOrDefault();

        var response = await _fileService.UploadAsync(model.Photo);
        if (response.Error)
            return dish;
        
        if (photo == null)
            await _photoRepository.CreateAsync(new Photo
            {
                Name = model.Photo.FileName,
                Metadata = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                DishId = dish.Id
            }, token);
        else
        {
            photo.Dish = dish;
            photo.DishId = dish.Id;
            photo.Metadata = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            await _photoRepository.UpdateAsync(photo, token);
        }
        
        return dish;
    }
}