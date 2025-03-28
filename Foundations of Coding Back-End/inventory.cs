using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(string name, double price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
}

class Program
{
    static List<Product> inventory = new List<Product>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nInventory Management System");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Stock");
            Console.WriteLine("3. View Products");
            Console.WriteLine("4. Remove Product");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddProduct(); break;
                case "2": UpdateStock(); break;
                case "3": ViewProducts(); break;
                case "4": RemoveProduct(); break;
                case "5": return;
                default: Console.WriteLine("Invalid choice. Try again."); break;
            }
        }
    }

    static void AddProduct()
    {
        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter product price: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter stock quantity: ");
        int stock = Convert.ToInt32(Console.ReadLine());

        inventory.Add(new Product(name, price, stock));
        Console.WriteLine("Product added successfully!");
    }

    static void UpdateStock()
    {
        Console.Write("Enter product name to update: ");
        string name = Console.ReadLine();
        Product product = inventory.Find(p => p.Name.ToLower() == name.ToLower());

        if (product != null)
        {
            Console.Write("Enter new stock quantity: ");
            product.Stock = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Stock updated successfully!");
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }

    static void ViewProducts()
    {
        Console.WriteLine("\nCurrent Inventory:");
        foreach (var product in inventory)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price:C}, Stock: {product.Stock}");
        }
    }

    static void RemoveProduct()
    {
        Console.Write("Enter product name to remove: ");
        string name = Console.ReadLine();
        Product product = inventory.Find(p => p.Name.ToLower() == name.ToLower());

        if (product != null)
        {
            inventory.Remove(product);
            Console.WriteLine("Product removed successfully!");
        }
        else
        {
            Console.WriteLine("Product not found!");
        }
    }
} 
