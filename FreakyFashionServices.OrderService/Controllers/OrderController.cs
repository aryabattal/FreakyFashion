using FreakyFashionServices.Models.Domain.DTO;
using FreakyFashionServices.OrderService.Data;
using FreakyFashionServices.OrderService.Models.Domain;
using FreakyFashionServices.OrderService.Models.Domain.DTO;
using FreakyFashionServices.OrderService.Models.DTO;
using FreakyFashionServices.OrderService.Models.DTO.BasketService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using BasketDto = FreakyFashionServices.Models.Domain.DTO.BasketDto;
using BasketService = FreakyFashionServices.OrderService.Models.DTO.BasketService;


namespace FreakyFashionServices.OrderService.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory , OrderContext context)
        {
            this.httpClientFactory = httpClientFactory;

            Context = context;
        }

        public OrderContext Context { get; }


        //____________________________________ Register Order ____________________________________________
        //POST api/ order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)

        {
            // Fetching BasketService: (GET /api/Baskets/{identifier})
            var basketDto = await FetchBasket(orderDto.Identifier);

            if (basketDto == null)
                return NotFound(); // 404 Not Found


            //map orderDto to Order
            var order = new Order(
                  identifier: orderDto.Identifier,
                  customer: orderDto.Customer);

            // Mappa basket items till OrderLines hos order
            var basketItem = basketDto.Items.Select(x =>
                 new BasketItemDto
                 {
                     ProductId = x.ProductId,
                     Quantity = x.Quantity
                 });

             var orders = new Order
            {
                Identifier= order.Identifier,
                Customer= order.Customer,
                Items = basketItem
                .Select(item => new OrderLine(item.ProductId, item.Quantity))
                .ToList()
            };

            //Save in DB

            Context.Orders.Add(orders);
            Context.SaveChanges();

          //return CreatedAtAction("orderId", new { order.Identifier });
           // return CreatedAtAction("GetOrderDTO", new { id = orderDto.Identifier }, orderDto.Identifier);
            return Created("", orders.Identifier);
        }

        //______________________________ Register basket _______________________________
        // POST /api/basket
        [HttpPost("basket")]
        public async Task<IActionResult> RegisterBasket(string identifier,BasketService.BasketDto basketDto)
        {
            var basketJson = new StringContent(
                JsonSerializer.Serialize(basketDto),
                Encoding.UTF8,
                Application.Json);

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.PostAsync("/api/basket", basketJson);

            return Created("", null); // 201 Created
        }
        //____________________________Fetch Basket____________________________________
        private async Task<BasketDto> FetchBasket(string identifier)
        {
            var httpRequestMessage = new HttpRequestMessage(
               HttpMethod.Get,
               $"http://localhost:6000/api/basket/{identifier}")
            {
                Headers = { { HeaderNames.Accept, "application/json" }, }
            };

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.SendAsync(httpRequestMessage);

            BasketDto basketDto = null;

            if (!httpResponseMessage.IsSuccessStatusCode)
                return basketDto;

            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var basketServiceBasketDto = await JsonSerializer.DeserializeAsync
                    <BasketService.BasketDto>(contentStream, options);

            basketDto = new BasketDto
            {
                Identifier = basketServiceBasketDto.Identifier,
                Items= basketServiceBasketDto.Items.ToList()

            };


            return basketDto; // 200 OK
        }


        //______________________Make Orderline_____________________________
        //POST / api / order /{ identifier}/ orderline
        //[HttpPost("{identifier}/orderline")]
        //   public IActionResult MakeOrderline(string identifier, NewOrderLineDto newOrderLineDto)
        //    {
           
        //    var order = Context.Orders.FirstOrDefault(x => x.Identifier == identifier);

        //    if (order == null)
        //    {
        //        order = new Order(identifier);

        //        Context.Orders.Add(order);
        //    }

        //    var orderLine = new OrderLine(
        //       productId: newOrderLineDto.ProductId,
        //       quantity: newOrderLineDto.Quantity
        //    );

        //    order.Items.Add(orderLine);

        //    Context.SaveChanges();

        //    var orderLineDto = new OrderLineDto
        //    {
        //        Id = orderLine.Id,
        //        ProductId = orderLine.ProductId,
        //         Quantity = orderLine.Quantity
            
        //    };

        //    return Created("", orderLineDto); // 201 Created
        //}
      
        //______________________________ GET basket _______________________________
        //[HttpGet("basket/{identifier}")]
        //public async Task<IActionResult> GetBasket(string identifier)
        //{

        //    var basketDto = await FetchBasket(identifier);

        //    if (basketDto == null)
        //        return NotFound(); // 404 Not Found

        //    basketDto.Items = await FetchOrderLine(identifier);

        //    return Ok(basketDto); // 200 OK
        //}


        //____________________________Fetch OrderLine_________________________________
        //private async Task<IEnumerable<OrderLineDto>> FetchOrderLine(string identifier)
        //{
        //    var httpRequestMessage = new HttpRequestMessage(
        //       HttpMethod.Get,
        //       $"https://localhost:9000/api/order/{identifier}")
        //    {
        //        Headers = { { HeaderNames.Accept, "application/json" }, }
        //    };

        //    var httpClient = httpClientFactory.CreateClient();

        //    using var httpResponseMessage =
        //        await httpClient.SendAsync(httpRequestMessage);

        //    var orderlinesDto = Enumerable.Empty<OrderLineDto>();

        //    if (!httpResponseMessage.IsSuccessStatusCode)
        //        return orderlinesDto;

        //    using var contentStream =
        //        await httpResponseMessage.Content.ReadAsStreamAsync();

        //    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        //    var order = await JsonSerializer.DeserializeAsync
        //            <Order>(contentStream, options);

        //    orderlinesDto = order.Items.Select(x =>
        //        new OrderLineDto
        //        {
        //            Id = x.Id,
        //            ProductId = x.ProductId,
        //            Quantity = x.Quantity
        //        }
        //    );

        //    return orderlinesDto; // 200 OK
        //}
    }
}
