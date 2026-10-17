using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Filters;
using ProductApi.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Ноутбук",     Price = 450000m },
        new Product { Id = 2, Name = "Смартфон",    Price = 250000m },
        new Product { Id = 3, Name = "Наушники",    Price = 35000m  },
        new Product { Id = 4, Name = "Клавиатура",  Price = 18000m  }
    };

    [HttpGet]
    [ApiKey]
    public ActionResult<IEnumerable<Product>> GetAll() => Ok(Products);

    [HttpGet("secure")]
    [Authorize]
    public ActionResult<IEnumerable<Product>> GetSecure() => Ok(Products);
}
