using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public string Category { get; set; } = string.Empty;
}
