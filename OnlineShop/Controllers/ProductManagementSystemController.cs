using Microsoft.AspNetCore.Mvc;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers;

public class ProductManagementSystemController : Controller
{
    // TODO: Add Db connection + services for business logics
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Save(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.Id = Guid.NewGuid();
            return RedirectToAction("Created", model);
        }

        return RedirectToAction("Create");
    }
    
    public IActionResult Created(ProductViewModel model)
    {
        return View(model);
    }

    public IActionResult All()
    {
        var products = new List<ProductViewModel>
        {
            new()
            {
                Currency = "USD", Id = Guid.NewGuid(), ProductDescription = "Test product", ProductName = "Socks",
                ProductPrice = 100
            }
        };

        return View(products);
    }
    
    public IActionResult Edit(Guid id)
    {
        var productViewModel = new ProductViewModel
        {
            Id = id,
            ProductName = "Sample Product",
            ProductDescription = "Sample Description",
            ProductPrice = 100.00m,
            Currency = "USD"
        };

        return View(productViewModel);
    }
    
    [HttpPost]
    public IActionResult Update(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {

            return RedirectToAction("All");
        }

        return View("Edit", model);
    }

    public IActionResult Delete(Guid id)
    {
        return RedirectToAction("Create");
    }
}