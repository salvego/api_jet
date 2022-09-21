using api_jet.Data;
using api_jet.Models;
using api_jet.Models.ViewModels;
using api_jet.Models.ViewModels.Products;
using Mercadinho.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mercadinho.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public async Task<IActionResult> GetProductsAsync(
            [FromServices] JetDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Products.AsNoTracking().CountAsync();
                var products = await context
                    .Products
                    .AsNoTracking()
                    .Select(x => new ListProductsViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Stock = x.Stock,
                        Status = x.Status,
                        Price = x.Price,
                        Image = x.Image
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.Title)
                    .ToListAsync();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Product>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/products/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] JetDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Conteúdo não encontrado"));

                return Ok(product);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Product>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/find-products/{title}")]
        public async Task<IActionResult> GetFindProductsAsync(
            [FromRoute] string title,
            [FromServices] JetDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Products.AsNoTracking().CountAsync();
                var products = await context
                    .Products
                    .AsNoTracking()
                    .Select(x => new ListProductsViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        Stock = x.Stock,
                        Status = x.Status,
                        Price = x.Price,
                        Image = x.Image
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.Title)
                    .Where(x => EF.Functions.Like(x.Title, "%"+ title +"%"))
                    .ToListAsync();
                return Ok(products);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Product>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/products")]
        public async Task<IActionResult> PostAsync(
            [FromBody] ListProductsViewModel model,
            [FromServices] JetDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Product>(ModelState.GetErrors()));

            try
            {
                var product = new Product
                {
                    Title = model.Title,
                    Description = model.Description,
                    Stock = model.Stock,
                    Status = model.Status,
                    Price = model.Price,
                    Image = model.Image
                };
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();

                return Created($"v1/products/{product.Id}", new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE9 - Não foi possível incluir um produto"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Product>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/products/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] ListProductsViewModel model,
            [FromServices] JetDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Conteúdo não encontrado"));

                product.Title = model.Title;
                product.Description = model.Description;
                product.Stock = model.Stock;
                product.Status = model.Status;
                product.Price = model.Price;
                product.Image = model.Image;

                context.Products.Update(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE8 - Não foi possível alterar o produto"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X11 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/products/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] JetDataContext context)
        {
            try
            {
                var product = await context
                    .Products
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                    return NotFound(new ResultViewModel<Product>("Conteúdo não encontrado"));

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Product>(product));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05XE7 - Não foi possível excluir o produto"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Product>("05X12 - Falha interna no servidor"));
            }
        }

    }
}