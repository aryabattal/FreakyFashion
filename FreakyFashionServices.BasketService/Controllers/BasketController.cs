using FreakyFashionServices.BasketService.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace FreakyFashionServices.BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
   {
        public BasketController(IDistributedCache cache)
        {
            Cache = cache;
        }

        public IDistributedCache Cache { get; }
         
        [HttpPut("{identifier}")]
        public IActionResult Basket(string identifier, BasketDto basketDto)
        {

            //Use jason for serialization, show it in jason format
            var serializiedBasket = JsonSerializer.Serialize(basketDto);
            if (identifier == basketDto.Identifier)
            //Use Identifier like a key
            Cache.SetString(basketDto.Identifier, serializiedBasket);

            return NoContent(); // 204 NoContent

        }


        [HttpGet("{identifier}")]
        public ActionResult<BasketDto> GetBasket(string identifier)
        {
            var serializedBasket = Cache.GetString(identifier);

            if (serializedBasket == null)
                return NotFound(); // 404 Not Found

            var basketDto = JsonSerializer.Deserialize<BasketDto>(serializedBasket);

            return Ok(basketDto);
        }
    }
}
