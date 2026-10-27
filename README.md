# LINQ Data Query Engine (.NET)

**Level 2 of 4 · Drill — Same tools, more moving parts**

A .NET console application that processes an in-memory retail product catalog using **LINQ (Language Integrated Query)** exclusively—eliminating imperative `for`/`foreach` loops in favor of declarative queries.

---

## 🎯 Requirements Checklist

| Requirement | Implementation Detail | Status |
| :--- | :--- | :---: |
| **Answer questions about a list of objects using LINQ** | Evaluates a dataset of 20 catalog products answering 6 real-world retail inventory questions | ✅ Met |
| **`Where`** | Used in Question 1, Question 5, and Question 6 to filter by status, price, category, and rating | ✅ Met |
| **`Select`** | Used in Questions 1, 2, 3, 5, and 6 to project custom formats, indexed lines, and view projections | ✅ Met |
| **`OrderBy` / `OrderByDescending`** | Used in Question 2 (by price desc), Question 3 (by category asc), Question 5 (by inventory value desc), and Question 6 (by title asc) | ✅ Met |
| **`GroupBy`** | Used in Question 3 (categorization summary) and Question 5 (inventory value aggregated per category) | ✅ Met |
| **At least one aggregate query** | Multiple aggregates implemented: `Count()`, `Sum()`, `Average()`, `Min()`, and `Max()` | ✅ Met |
| **Results printed with labels** | Clear boxed headers, categorized labels, and aligned table-like lines for every query | ✅ Met |
| **Builds and runs cleanly** | Verified on modern .NET SDK (`net10.0` / `net8.0`) | ✅ Met |

---

## 📊 The 6 Business Questions & LINQ Implementation

### 1. Filtering & Projection (`Where` + `Select`)
- **Question**: Which active products are currently in stock and cost under $30.00?
- **LINQ**:
  ```csharp
  var affordableInStock = products
      .Where(p => p.IsActive && p.StockQuantity > 0 && p.Price < 30.00m)
      .Select(p => $"• {p.Name} | {p.Category} | {p.Price:C} | Stock: {p.StockQuantity}")
      .ToList();
  ```

### 2. Sorting & Pagination (`OrderByDescending` + `Select`)
- **Question**: What are the top 5 most expensive products in the catalog?
- **LINQ**:
  ```csharp
  var top5MostExpensive = products
      .OrderByDescending(p => p.Price)
      .Take(5)
      .Select((p, index) => $"#{index + 1} {p.Name} | {p.Price:C} | {p.Rating:F1}★")
      .ToList();
  ```

### 3. Grouping & Categorization (`GroupBy` + `Select`)
- **Question**: How many products belong to each category, and what is their average customer rating and price range?
- **LINQ**:
  ```csharp
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
      .ToList();
  ```

### 4. Comprehensive Aggregates (`Sum`, `Average`, `Min`, `Max`)
- **Question**: What are the store-wide financial and stock metrics?
- **LINQ**:
  ```csharp
  int totalUnitsInStock       = products.Sum(p => p.StockQuantity);
  decimal totalInventoryValue = products.Sum(p => p.Price * p.StockQuantity);
  decimal averageProductPrice = products.Average(p => p.Price);
  decimal cheapestPrice       = products.Min(p => p.Price);
  decimal highestPrice        = products.Max(p => p.Price);
  var topRatedProduct         = products.OrderByDescending(p => p.Rating).First();
  ```

### 5. Multi-Stage Pipeline (`Where` + `GroupBy` + `OrderByDescending` + `Select`)
- **Question**: For active in-stock products, what is the total inventory value per category, sorted from highest to lowest?
- **LINQ**:
  ```csharp
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
      .ToList();
  ```

### 6. Targeted Filter & Alphabetic Ordering (`Where` + `OrderBy` + `Select`)
- **Question**: Which books have a rating of 4.8 or higher, sorted alphabetically by title?
- **LINQ**:
  ```csharp
  var topBooksAlphabetical = products
      .Where(p => p.Category == "Books" && p.Rating >= 4.8)
      .OrderBy(p => p.Name)
      .Select(p => $"• {p.Name} (Rating: {p.Rating:F1}★, Price: {p.Price:C})")
      .ToList();
  ```

---

## 💡 Key Decisions & Rationale

1. **Declarative vs Imperative**:
   Traditional procedural programming relies on nested loops with accumulator variables (`decimal total = 0; foreach (...) total += ...`). LINQ abstracts state management away into composable, functional pipelines that are self-documenting, less error-prone, and easier to refactor.

2. **Decimals for Financial Computations**:
   Stock valuations and unit prices are modeled using `decimal` rather than `double` to eliminate floating-point precision issues in financial aggregation (`Sum()`, `Average()`).

3. **Anonymous Projections in `GroupBy`**:
   Rather than materializing full entity collections within grouped data, `Select` extracts only the aggregated metrics (`Count`, `Average`, `Sum`, `Min`, `Max`) required for presentation, optimizing memory overhead.

---

## 🚀 How to Run

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0, 9.0, or 10.0)

### Clone & Run
```bash
# 1. Clone the repository
git clone https://github.com/avi74600/linq-drill-console-app.git
cd linq-drill-console-app

# 2. Build the project
dotnet build

# 3. Run the console application
dotnet run
```

---

## 📋 Sample Output

```text
==========================================================================
                 LINQ DATA QUERY ENGINE — LEVEL 2 DRILL                   
               Querying Objects Declaratively Without Loops               
==========================================================================

Total Products in Catalog: 20

╔════════════════════════════════════════════════════════════════════════╗
║ QUESTION 1: Which active products are currently in stock and cost under $30.00? ║
╚════════════════════════════════════════════════════════════════════════╝
[Result Count: 7]
  • USB-C Fast Charging Cable (2-Pack)  | Electronics  | Price:   $14.99 | Stock:  95
  • Pocket Paperback Dictionary         | Books        | Price:    $8.99 | Stock:  50
  • Stainless Steel French Press        | Home         | Price:   $29.99 | Stock:  40
  ...

╔════════════════════════════════════════════════════════════════════════╗
║ QUESTION 4: What are the store-wide inventory metrics (Total Units, Total Valuation, Average Price, Price Extrema)? ║
╚════════════════════════════════════════════════════════════════════════╝
  [Total Units in Stock]    : 714 units
  [Total Inventory Value]   : $33,045.92
  [Average Product Price]   : $86.35
  [Cheapest Product Price]  : $8.99
  [Highest Product Price]   : $329.00
  [Top Rated Product]       : Clean Code by Robert C. Martin (4.9★ at $38.50)

==========================================================================
   All 6 queries executed successfully using LINQ (0 imperative loops).   
==========================================================================
```
