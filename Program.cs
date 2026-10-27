using System;
using System.Linq;
using LinqDrill.Data;
using LinqDrill.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

PrintBanner();

// Load in-memory dataset of catalog products
List<Product> products = ProductCatalog.GetProducts();

Console.WriteLine($"Total Products in Catalog: {products.Count}\n");

// ============================================================================
// QUESTION 1: Filtering with Where & projecting with Select
// ============================================================================
PrintQuestionHeader(1, $"Which active products are currently in stock and cost under {30.00m:C}?");

var affordableInStock = products
    .Where(p => p.IsActive && p.StockQuantity > 0 && p.Price < 30.00m)
    .Select(p => $"  • {p.Name,-35} | {p.Category,-12} | Price: {p.Price,8:C} | Stock: {p.StockQuantity,3}")
    .ToList();

Console.WriteLine($"[Result Count: {affordableInStock.Count}]");
affordableInStock.ForEach(Console.WriteLine);
Console.WriteLine();

// ============================================================================
// QUESTION 2: Sorting with OrderBy / OrderByDescending & projecting with Select
// ============================================================================
PrintQuestionHeader(2, "What are the top 5 most expensive products in the catalog (highest to lowest)?");

var top5MostExpensive = products
    .OrderByDescending(p => p.Price)
    .Take(5)
    .Select((p, index) => $"  #{index + 1} {p.Name,-35} | {p.Category,-12} | Price: {p.Price,8:C} | Rating: {p.Rating:F1}★")
    .ToList();

top5MostExpensive.ForEach(Console.WriteLine);
Console.WriteLine();

// ============================================================================
// QUESTION 3: Categorization with GroupBy & aggregate projections
// ============================================================================
PrintQuestionHeader(3, "How many products belong to each category, and what is the average customer rating per category?");

var categorySummary = products
    .GroupBy(p => p.Category)
    .Select(group => new
    {
        Category = group.Key,
        ProductCount = group.Count(),
        AverageRating = group.Average(p => p.Rating),
        MinPrice = group.Min(p => p.Price),
        MaxPrice = group.Max(p => p.Price)
    })
    .OrderBy(c => c.Category)
    .Select(c => $"  • {c.Category,-14} -> Count: {c.ProductCount,2} | Avg Rating: {c.AverageRating:F2}★ | Price Range: {c.MinPrice:C} - {c.MaxPrice:C}")
    .ToList();

categorySummary.ForEach(Console.WriteLine);
Console.WriteLine();

// ============================================================================
// QUESTION 4: Store-wide Financial & Inventory Aggregates (Sum, Average, Min, Max)
// ============================================================================
PrintQuestionHeader(4, "What are the store-wide inventory metrics (Total Units, Total Valuation, Average Price, Price Extrema)?");

int totalUnitsInStock       = products.Sum(p => p.StockQuantity);
decimal totalInventoryValue = products.Sum(p => p.Price * p.StockQuantity);
decimal averageProductPrice = products.Average(p => p.Price);
decimal cheapestPrice       = products.Min(p => p.Price);
decimal highestPrice        = products.Max(p => p.Price);
var topRatedProduct         = products.OrderByDescending(p => p.Rating).ThenBy(p => p.Price).First();

Console.WriteLine($"  [Total Units in Stock]    : {totalUnitsInStock:N0} units");
Console.WriteLine($"  [Total Inventory Value]   : {totalInventoryValue:C}");
Console.WriteLine($"  [Average Product Price]   : {averageProductPrice:C}");
Console.WriteLine($"  [Cheapest Product Price]  : {cheapestPrice:C}");
Console.WriteLine($"  [Highest Product Price]   : {highestPrice:C}");
Console.WriteLine($"  [Top Rated Product]       : {topRatedProduct.Name} ({topRatedProduct.Rating:F1}★ at {topRatedProduct.Price:C})");
Console.WriteLine();

// ============================================================================
// QUESTION 5: Complex Query (Where + GroupBy + OrderByDescending + Select)
// ============================================================================
PrintQuestionHeader(5, "For active in-stock products, what is the total inventory value per category (sorted highest to lowest)?");

var inventoryValueByCategory = products
    .Where(p => p.IsActive && p.StockQuantity > 0)
    .GroupBy(p => p.Category)
    .Select(group => new
    {
        Category = group.Key,
        TotalStockValue = group.Sum(p => p.Price * p.StockQuantity),
        TotalUnits = group.Sum(p => p.StockQuantity),
        ProductLines = group.Count()
    })
    .OrderByDescending(x => x.TotalStockValue)
    .Select((x, idx) => $"  #{idx + 1} {x.Category,-12} | Inventory Value: {x.TotalStockValue,12:C} | Total Units: {x.TotalUnits,4} | Active Products: {x.ProductLines}")
    .ToList();

inventoryValueByCategory.ForEach(Console.WriteLine);
Console.WriteLine();

// ============================================================================
// QUESTION 6: Targeted Filter & Alphabetic Sort (Where + OrderBy + Select)
// ============================================================================
PrintQuestionHeader(6, "Which books have a rating of 4.8 or higher, sorted alphabetically by title?");

var topBooksAlphabetical = products
    .Where(p => p.Category == "Books" && p.Rating >= 4.8)
    .OrderBy(p => p.Name)
    .Select(p => $"  • {p.Name,-35} | Rating: {p.Rating:F1}★ | Price: {p.Price:C}")
    .ToList();

topBooksAlphabetical.ForEach(Console.WriteLine);
Console.WriteLine();

PrintFooter();

// ============================================================================
// Formatting Helpers
// ============================================================================

static void PrintBanner()
{
    Console.WriteLine("==========================================================================");
    Console.WriteLine("                 LINQ DATA QUERY ENGINE — LEVEL 2 DRILL                   ");
    Console.WriteLine("               Querying Objects Declaratively Without Loops               ");
    Console.WriteLine("==========================================================================");
    Console.WriteLine();
}

static void PrintQuestionHeader(int number, string question)
{
    Console.WriteLine($"╔════════════════════════════════════════════════════════════════════════╗");
    Console.WriteLine($"║ QUESTION {number}: {question,-58} ║");
    Console.WriteLine($"╚════════════════════════════════════════════════════════════════════════╝");
}

static void PrintFooter()
{
    Console.WriteLine("==========================================================================");
    Console.WriteLine("   All 6 queries executed successfully using LINQ (0 imperative loops).   ");
    Console.WriteLine("==========================================================================");
}
