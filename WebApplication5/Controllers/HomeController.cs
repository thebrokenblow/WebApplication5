using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var products = new List<Product>
        {
            new Product
            {
                Name = "Iphone",
                Category = "Телефоны",
                Cost = 100_000
            },
            new Product
            {
                Name = "Телевизор Samsumg",
                Category = "Телевизоры",
                Cost = 100_000
            },
            new Product
            {
                Name = "Samsung Телефон",
                Category = "Телефоны",
                Cost = 100_000
            }
        };

        return View(products);
    }

    public IActionResult Details(string name, string category)
    {

        return View();
    }
}
