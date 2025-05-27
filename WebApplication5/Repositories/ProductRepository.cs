using WebApplication5.Models;

namespace WebApplication5.Repositories;

public class ProductRepository
{
    private readonly List<Product> products =
   [
       new Product
            {
                Id = 1,
                Name = "Iphone",
                Category = "Телефоны",
                Cost = 120_000
            },
            new Product
            {
                Id = 2,
                Name = "Телевизор Samsumg",
                Category = "Телевизоры",
                Cost = 130_000
            },
            new Product
            {
                Id = 3,
                Name = "Samsung Телефон",
                Category = "Телефоны",
                Cost = 140_000
            }
   ];

    public ProductRepository()
    {
        for (int i = 0; i < 10; i++)
        {
            products.AddRange(products);
        }
    }

    public List<Product> GetAll()
    {
        return products;
    }

    public void Add(Product product)
    {
        products.Add(product);
    }

    public Product GetById(int id)
    {
        var product = products.First(product => product.Id == id);

        return product;
    }

    public void RemoveById(int id)
    {
        var product = products.First(product => product.Id == id);
        products.Remove(product);
    }
}