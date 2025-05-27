using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication5.Common;
using WebApplication5.Models;

namespace WebApplication5.ViewModels;

public class ProductFilterViewModel
{
    public required IEnumerable<Product> Products { get; init; }
    public required SelectList Categories { get; init; }
    public required PageViewModel PageViewModel { get; init; }
    public string? FilterName { get; init; }
}