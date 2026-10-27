namespace LinqDrill.Models;

/// <summary>
/// Represents a retail catalog product.
/// </summary>
public class Product
{
    public int Id { get; }
    public string Name { get; }
    public string Category { get; }
    public decimal Price { get; }
    public int StockQuantity { get; }
    public double Rating { get; }
    public bool IsActive { get; }

    public Product(int id, string name, string category, decimal price, int stockQuantity, double rating, bool isActive)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        StockQuantity = stockQuantity;
        Rating = rating;
        IsActive = isActive;
    }

    public override string ToString()
    {
        return $"[#{Id}] {Name} ({Category}) - {Price:C} | Stock: {StockQuantity} | Rating: {Rating:F1}★";
    }
}
