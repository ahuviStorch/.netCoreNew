using System.Text.Json;
using SweetBakery.Interfaces;
using SweetBakery.Models;

namespace SweetBakery.Services;

public class ProductService : IProductService
{
    private readonly string _filePath;

    public ProductService(IWebHostEnvironment env)
    {
        _filePath = Path.Combine(
            env.ContentRootPath,
            "Data",
            "products.json"
        );
    }

    // private List<Product> ReadProducts()
    // {
    //     if (!File.Exists(_filePath))
    //     {
    //         return new List<Product>();
    //     }

    //     string json = File.ReadAllText(_filePath);

    //     return JsonSerializer.Deserialize<List<Product>>(json)
    //            ?? new List<Product>();
    // }
    private List<Product> ReadProducts()
{
    Console.WriteLine("Looking for file: " + _filePath);

    if (!File.Exists(_filePath))
    {
        Console.WriteLine("FILE NOT FOUND!");
        return new List<Product>();
    }

    Console.WriteLine("FILE FOUND!");

    string json = File.ReadAllText(_filePath);

    Console.WriteLine("JSON content: " + json);

    return JsonSerializer.Deserialize<List<Product>>(json)
           ?? new List<Product>();
}

    private void SaveProducts(List<Product> products)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(products, options);

        File.WriteAllText(_filePath, json);
    }

    public List<Product> GetAll()
    {
        return ReadProducts();
    }

    public Product? GetById(int id)
    {
        var products = ReadProducts();

        foreach (var product in products)
        {
            if (product.Id == id)
            {
                return product;
            }
        }

        return null;
    }

    public List<Product> GetByCategory(string category)
    {
        var products = ReadProducts();

        var result = new List<Product>();

        foreach (var product in products)
        {
            if (string.Equals(
                product.Category,
                category,
                StringComparison.OrdinalIgnoreCase))
            {
                result.Add(product);
            }
        }

        return result;
    }

    public Product Add(Product product)
    {
        var products = ReadProducts();

        int maxId = 0;

        foreach (var item in products)
        {
            if (item.Id > maxId)
            {
                maxId = item.Id;
            }
        }

        product.Id = maxId + 1;

        products.Add(product);

        SaveProducts(products);

        return product;
    }

    public bool Update(int id, Product updatedProduct)
    {
        var products = ReadProducts();

        foreach (var product in products)
        {
            if (product.Id == id)
            {
                product.Name = updatedProduct.Name;
                product.Category = updatedProduct.Category;
                product.Price = updatedProduct.Price;
                product.QuantityInStock =
                    updatedProduct.QuantityInStock;

                SaveProducts(products);

                return true;
            }
        }

        return false;
    }

    public bool Delete(int id)
    {
        var products = ReadProducts();

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Id == id)
            {
                products.RemoveAt(i);

                SaveProducts(products);

                return true;
            }
        }

        return false;
    }
}