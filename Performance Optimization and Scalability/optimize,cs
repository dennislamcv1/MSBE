// Below is an inefficient loop used to process orders:

foreach (var order in orders)
{
    var product = db.Products.FirstOrDefault(p => p.Id == order.ProductId);
    Console.WriteLine($"Order {order.Id}: {product.Name} - {order.Quantity}");
}

// Optimized Code Using Eager Loading via Dictionary Lookup

// Step 1: Load all relevant product IDs in a single query
var productIds = orders.Select(o => o.ProductId).Distinct().ToList();
var productDict = db.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToDictionary(p => p.Id);

// Step 2: Loop efficiently
foreach (var order in orders)
{
    if (productDict.TryGetValue(order.ProductId, out var product))
    {
        Console.WriteLine($"Order {order.Id}: {product.Name} - {order.Quantity}");
    }
    else
    {
        Console.WriteLine($"Order {order.Id}: Product not found - {order.Quantity}");
    }
}

