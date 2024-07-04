using Microsoft.AspNetCore.Mvc;
using SimpleCRUD_API.Data;

namespace SimpleCRUD_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("")]
    public ActionResult<IEnumerable<Product>> Get()
    {
        var records = _context.Set<Product>().ToList();
        return Ok(records);
    }

    [HttpGet]
    [Route("{Id}")]
    public ActionResult<Product> GetProductById(int Id)
    {
        var record = _context.Set<Product>().Find(Id);
        if (record != null)
        {
            return Ok(record);
        }

        return NotFound();
    }
    [HttpPost]
    [Route("")]
    public ActionResult<int> CreateProduct(Product product)
    {
        product.Id = 0;
        _context.Set<Product>().Add(product);
        _context.SaveChanges();
        return Ok(product.Id);
    }

    [HttpPut]
    [Route("")]
    public ActionResult UpdateProduct(Product product)
    {
        var existingProduct = _context.Set<Product>().Find(product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Sku = product.Sku;
            _context.Set<Product>().Update(existingProduct);
            _context.SaveChanges();
            return Ok();
        }

        return NotFound();
    }

    [HttpDelete]
    [Route("{Id}")]
    public ActionResult DeleteProduct(int Id)
    {
        var existingProduct = _context.Set<Product>().Find(Id);
        if (existingProduct != null)
        {
            _context.Set<Product>().Remove(existingProduct);
            _context.SaveChanges();
            return Ok();
        }
        return NotFound();
    }
}