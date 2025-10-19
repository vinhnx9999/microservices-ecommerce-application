using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinhMicroservices.Model;
using VinhMicroservices.ProductService.Data;

namespace VinhMicroservices.ProductService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(VinhMicroProductDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
    {
        return await dbContext.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        return product == null ? NotFound() : Ok(product);
    }
}
