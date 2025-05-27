using Microsoft.AspNetCore.Mvc;
using WebApplication5.Extensions;
using WebApplication5.Models;
using WebApplication5.Repositories;

namespace WebApplication5.Controllers;

public class ProductController(ProductRepository productRepository) : Controller
{
    public IActionResult Index()
    {
        var products = productRepository.GetAll();

        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        productRepository.Add(product);

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

        var controllerName = this.GetControllerName<ProductController>();

        return RedirectToAction(nameof(Index), controllerName);
    }
}