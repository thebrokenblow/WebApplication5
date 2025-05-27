using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication5.Common;
using WebApplication5.Helpers;
using WebApplication5.Models;
using WebApplication5.Repositories;
using WebApplication5.ViewModels;

namespace WebApplication5.Controllers;

public class ProductController(ProductRepository productRepository, CategoryRepository categoryRepository) : Controller
{
    private const int pageSize = 20;

    public IActionResult Index(string productName, string nameCategory, SortedType sortedType, int page = 1)
    {
        var products = productRepository.GetAll();
        var categories = categoryRepository.GetAll();

        var filteringProducts = products.AsEnumerable();

        if (!string.IsNullOrEmpty(productName))
        {
            filteringProducts = filteringProducts.Where(product => product.Name.Contains(productName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrEmpty(nameCategory))
        {
            filteringProducts = filteringProducts.Where(product => product.Category == nameCategory);
        }

        switch (sortedType)
        {
            case SortedType.CostAsk:
                filteringProducts = filteringProducts.OrderBy(product => product.Cost);
            break;

            case SortedType.CostDesc:
                filteringProducts = filteringProducts.OrderByDescending(product => product.Cost);
            break;

            default:
            break;
        }

        filteringProducts = filteringProducts.Skip((page - 1) * pageSize).Take(pageSize);

        var currentProductFilterViewModel = new ProductFilterViewModel
        {
            Categories = new SelectList(categories),
            Products = filteringProducts,
            FilterName = productName,
            PageViewModel = new PageViewModel(filteringProducts.Count(), page, pageSize)
        };

        return View(currentProductFilterViewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        productRepository.Add(product);

        var nameController = nameof(ProductController);
        var name = nameController.Replace("Controller", "");

        return RedirectToAction(nameof(Index), "Product");
    }

    public IActionResult Edit(int id)
    {
        var product = productRepository.GetById(id);

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var editProduct = productRepository.GetById(product.Id);

        editProduct.Name = product.Name;
        editProduct.Category = product.Category;
        editProduct.Cost = product.Cost;

        var nameController = nameof(ProductController);
        var name = nameController.Replace("Controller", "");

        return RedirectToAction(nameof(Index), "Product");
    }

    public IActionResult Delete(int id, string name, string category, decimal cost)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Category = category,
            Cost = cost
        };

        return View(product);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        productRepository.RemoveById(id);

        var nameController = ControllerHelper.GetName<ProductController>();
        return RedirectToAction(nameof(Index), nameController);
    }
}