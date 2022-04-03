
namespace FreakyFashionServices.OrderService.Models.Domain.DTO
{
    public class OrderLineDto
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
