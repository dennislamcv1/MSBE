// Analyze the following non-resilient function:

public void ProcessOrder(Order order)
{
    var product = db.Products.Find(order.ProductId);
    product.Stock -= order.Quantity;
    Console.WriteLine($"Order {order.Id} processed.");
}

// Fixed and Resilient Version

public void ProcessOrder(Order order)
{
    try
    {
        if (order == null)
        {
            Console.WriteLine("Invalid order: Order is null.");
            return;
        }

        if (order.Quantity <= 0)
        {
            Console.WriteLine($"Order {order.Id} failed: Invalid quantity.");
            return;
        }

        var product = db.Products.Find(order.ProductId);
        if (product == null)
        {
            Console.WriteLine($"Order {order.Id} failed: Product not found.");
            return;
        }

        if (product.Stock < order.Quantity)
        {
            Console.WriteLine($"Order {order.Id} failed: Not enough stock. Available: {product.Stock}");
            return;
        }

        product.Stock -= order.Quantity;
        db.SaveChanges();

        Console.WriteLine($"Order {order.Id} processed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing order {order?.Id}: {ex.Message}");
        // Optionally log ex.StackTrace or use structured logging
    }
}

// Use Copilot to recommend and apply efficiencies to Implement null checks and validation:

if (product == null)
    throw new Exception("Product not found.");
if (product.Stock < order.Quantity)
    throw new Exception("Insufficient stock.");

// Improved Version with Copilot's Best Practices

public void ProcessOrder(Order order)
{
    try
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order), "Order cannot be null.");

        if (order.Quantity <= 0)
            throw new ArgumentException("Order quantity must be greater than zero.", nameof(order.Quantity));

        var product = db.Products.Find(order.ProductId);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {order.ProductId} not found.");

        if (product.Stock < order.Quantity)
            throw new InvalidOperationException($"Insufficient stock for product {product.Id}. Requested: {order.Quantity}, Available: {product.Stock}");

        product.Stock -= order.Quantity;
        db.SaveChanges();

        Console.WriteLine($"Order {order.Id} processed successfully.");
    }
    catch (ArgumentNullException ex)
    {
        Console.WriteLine($"Input error: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Validation failed: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Operation failed: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error while processing order {order?.Id}: {ex.Message}");
        // Optional: log ex.StackTrace
    }
}
