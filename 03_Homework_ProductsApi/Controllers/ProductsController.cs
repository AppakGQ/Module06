using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;

namespace ProductsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Ноутбук",    Price = 450000m, Category = "Электроника" },
        new Product { Id = 2, Name = "Смартфон",   Price = 250000m, Category = "Электроника" },
        new Product { Id = 3, Name = "Рюкзак",     Price = 20000m,  Category = "Аксессуары"  }
    };
    private static int _nextId = 4;
    private static readonly object Lock = new();

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        lock (Lock) return Ok(Products.ToList());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        lock (Lock)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            return product is null ? NotFound(new { message = $"Product {id} not found" }) : Ok(product);
        }
    }

    [HttpPost]
    [Authorize]
    public IActionResult Create([FromBody] Product product)
    {
        var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

        lock (Lock)
        {
            product.Id = _nextId++;
            Products.Add(product);
        }

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, new
        {
            message = "Product created",
            createdBy = userName,
            product
        });
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        lock (Lock)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
                return NotFound(new { message = $"Product {id} not found" });

            Products.Remove(product);
            return NoContent();
        }
    }
}
