using Microsoft.AspNetCore.Mvc;
using SweetBakery.Interfaces;
using SweetBakery.Models;

namespace SweetBakery.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productService.GetAll();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetById(id);

        if (product == null)
        {
            return NotFound("Product not found");
        }

        return Ok(product);
    }

    [HttpGet("category/{category}")]
    public IActionResult GetByCategory(string category)
    {
        var products = _productService.GetByCategory(category);

        return Ok(products);
    }

    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (!IsValidProduct(product))
        {
            return BadRequest("Invalid product details");
        }

        var newProduct = _productService.Add(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = newProduct.Id },
            newProduct
        );
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Product product)
    {
        if (!IsValidProduct(product))
        {
            return BadRequest("Invalid product details");
        }

        bool updated = _productService.Update(id, product);

        if (!updated)
        {
            return NotFound("Product not found");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        bool deleted = _productService.Delete(id);

        if (!deleted)
        {
            return NotFound("Product not found");
        }

        return NoContent();
    }

    private static bool IsValidProduct(Product product)
    {
        return !string.IsNullOrWhiteSpace(product.Name)
            && !string.IsNullOrWhiteSpace(product.Category)
            && product.Price >= 0
            && product.QuantityInStock >= 0;
    }
}