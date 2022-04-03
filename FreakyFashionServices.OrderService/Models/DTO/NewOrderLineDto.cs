namespace FreakyFashionServices.OrderService.Models.DTO
{
    public class NewOrderLineDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
