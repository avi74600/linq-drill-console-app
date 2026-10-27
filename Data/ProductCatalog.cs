namespace LinqDrill.Data;

using LinqDrill.Models;

/// <summary>
/// Provides a populated in-memory catalog of products for querying with LINQ.
/// </summary>
public static class ProductCatalog
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product(1,  "Noise-Canceling Wireless Headphones", "Electronics", 199.99m, 45, 4.8, true),
            new Product(2,  "Mechanical Gaming Keyboard",          "Electronics",  89.50m, 12, 4.6, true),
            new Product(3,  "Ultra-HD 4K Monitor 27-inch",          "Electronics", 329.00m,  8, 4.7, true),
            new Product(4,  "USB-C Fast Charging Cable (2-Pack)",   "Electronics",  14.99m, 95, 4.3, true),
            new Product(5,  "Vintage Turntable Stereo",             "Electronics", 149.00m,  0, 4.1, false),

            new Product(6,  "Clean Code by Robert C. Martin",       "Books",        38.50m, 25, 4.9, true),
            new Product(7,  "Design Patterns (Gang of Four)",       "Books",        49.99m, 18, 4.8, true),
            new Product(8,  "The Pragmatic Programmer",             "Books",        42.00m, 30, 4.9, true),
            new Product(9,  "Intro to Algorithms",                  "Books",        75.00m,  5, 4.5, true),
            new Product(10, "Pocket Paperback Dictionary",          "Books",         8.99m, 50, 3.8, true),

            new Product(11, "Stainless Steel French Press",         "Home",         29.99m, 40, 4.6, true),
            new Product(12, "Smart Air Fryer XL",                   "Home",        119.99m, 15, 4.7, true),
            new Product(13, "Bamboo Cutting Board Set",             "Home",         24.50m, 60, 4.4, true),
            new Product(14, "Ceramic Pour-Over Coffee Dripper",     "Home",         18.00m,  0, 4.2, false),

            new Product(15, "Adjustable Dumbbells Set",             "Fitness",     249.99m, 10, 4.8, true),
            new Product(16, "High-Density Yoga Mat",                "Fitness",      28.00m, 70, 4.5, true),
            new Product(17, "Resistance Exercise Bands (Set of 5)", "Fitness",      15.99m, 85, 4.3, true),

            new Product(18, "Ergonomic Mesh Office Chair",          "Office",      189.00m, 14, 4.6, true),
            new Product(19, "Dual Monitor Aluminum Stand",          "Office",       45.00m, 22, 4.4, true),
            new Product(20, "Refillable Ballpoint Pen 10-Pack",     "Office",        9.50m, 110, 4.0, true)
        };
    }
}
