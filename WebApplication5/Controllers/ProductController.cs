using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication5.Helpers;
using WebApplication5.Models;
using WebApplication5.Repositories;
using WebApplication5.ViewModels;

namespace WebApplication5.Controllers;

public class ProductController(ProductRepository productRepository, CategoryRepository categoryRepository) : Controller
{
    private const int pageSize = 20;

    public IActionResult Index(ProductFilterViewModel productFilterViewModel)
    {
        var products = productRepository.GetAll();
        var categories = categoryRepository.GetAll();

        var filteringProducts = products.AsEnumerable();

        if (!string.IsNullOrEmpty(productFilterViewModel.ProductName))
        {
            filteringProducts = filteringProducts.Where(
                product => product.Name.Contains(
                    productFilterViewModel.ProductName, StringComparison.CurrentCultureIgnoreCase));
        }

        if (!string.IsNullOrEmpty(productFilterViewModel.NameCategory))
        {
            filteringProducts = filteringProducts.Where(
                product => product.Category == productFilterViewModel.NameCategory);
        }

        filteringProducts = FilteringProducts(filteringProducts, productFilterViewModel.SortedType);

        var paginationFilteringProducts = filteringProducts.Skip((productFilterViewModel.Page - 1) * pageSize).Take(pageSize);

        var currentProductFilterViewModel = new ProductFilterViewModel
        {
            Categories = new SelectList(categories),
            Products = paginationFilteringProducts,
            FilterName = productFilterViewModel.ProductName,
            PageViewModel = new PageViewModel(filteringProducts.Count(), productFilterViewModel.Page, pageSize),
            NameCategory = productFilterViewModel.NameCategory,
            SortedType = productFilterViewModel.SortedType,
            Page = productFilterViewModel.Page
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


    /// <summary>
    /// Фильтрация Продуктов по значениям SortedType
    /// </summary>
    /// <param name="sortedType"></param>
    /// <returns></returns>
    private IEnumerable<Product> FilteringProducts(IEnumerable<Product> products, SortedType sortedType) => sortedType switch
    {
        SortedType.CostAsk => products.OrderBy(product => product.Cost),
        SortedType.CostDesc => products.OrderByDescending(product => product.Cost),
        SortedType.None => products
    };
}