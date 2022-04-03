using FreakyFashionServices.OrderService.Models.Domain;

namespace FreakyFashionServices.OrderService.Models.DTO
{
    public class OrderDto
    {
        public OrderDto(string identifier, string customer)
        {
            Identifier = identifier;
            Customer = customer;
        }

        //public int Id { get; set; }
        public string Identifier { get; set; }
        public string Customer { get; set; }


    }
}
