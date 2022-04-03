using FreakyFashionServices.CatalogService.Data;
using FreakyFashionServices.CatalogService.Extensions;
using FreakyFashionServices.CatalogService.Models.Domain;
using FreakyFashionServices.CatalogService.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionServices.CatalogService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        public CatalogController(CatalogServiceContext context)
        {
            Context = context;
        }

        private CatalogServiceContext Context { get; }

        // POST: api/product
        [HttpPost]
        public IActionResult CreateProduct(NewProductDto newProductDto)
        {
            var product = Context.Catalog
                .FirstOrDefault(x => x.ArticleNumber == newProductDto.ArticleNumber);

            // Map newProductDto to Product
            product = new Product(
                articleNumber: newProductDto.ArticleNumber,
                description: newProductDto.Description,
                name: newProductDto.Name,
                imageUrl: newProductDto.ImageUrl,
                price: newProductDto.Price
                   );
      
            Context.Catalog.Add(product);

            Context.SaveChanges();

            //Mappa från product till productDto
            var productDto = new ProductDto
                 (id: product.Id,
                   articleNumber: product.ArticleNumber,
                   description: product.Description,
                    name: product.Name,
                   imageUrl: product.ImageUrl,
                   urlSlug: product.UrlSlug,
                   price: product.Price
                );

            return Created("", productDto); // 201 Created
        }


        // Get: api/product/{articleNumber}
        [HttpGet("{articleNumber}")]
     
        public IActionResult GetProduct(string ArticleNumber)
        {
            var product = Context.Catalog.FirstOrDefault(x => x.ArticleNumber == ArticleNumber);

            if (product == null)
                return NotFound(); // 404 Not Found

            var productDto = new ProductDto
            (
             id: product.Id,
             articleNumber: product.ArticleNumber,
             description: product.Description,
             name: product.Name,
             imageUrl: product.ImageUrl,
             urlSlug: product.UrlSlug,
             price: product.Price
    );

            return Ok(productDto); // 200 OK
        }
        
    }

  
}
