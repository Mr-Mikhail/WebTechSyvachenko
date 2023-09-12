using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Controllers.Dto;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Controllers;

public class CategoryManagementSystemController : Controller
{
    private readonly IMapper _mapper;
    private readonly IRepository<Category> _repository;

    public CategoryManagementSystemController(IMapper mapper, IRepository<Category> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IActionResult> All(CancellationToken token)
    {
        var categories = await _repository.GetAllAsync(token);
        var viewModel = _mapper.Map<List<CategoryModel>>(categories);

        return View(viewModel);
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken token)
    {
        var categories = await _repository.GetAsync(x => x.Id == id, token);
        var category = categories.FirstOrDefault();
        if (category == null)
            return View("All");

        var viewModel = _mapper.Map<CategoryModel>(category);
        
        return View(viewModel);
    }

    public async Task<IActionResult> Update(CategoryModel model, CancellationToken token)
    {
        if (!ModelState.IsValid) 
            return View("Edit", model);
        
        var category = _mapper.Map<Category>(model);
        await _repository.UpdateAsync(category, token);
        return RedirectToAction("All");
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        await _repository.DeleteAsync(id, token);
        
        return RedirectToAction("Index", "DishManagementSystem");
    }
}