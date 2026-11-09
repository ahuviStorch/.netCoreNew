using SweetBakery.Models;

namespace SweetBakery.Interfaces;

public interface IProductService
{
    List<Product> GetAll();

    Product? GetById(int id);

    List<Product> GetByCategory(string category);

    Product Add(Product product);

    bool Update(int id, Product product);

    bool Delete(int id);
}