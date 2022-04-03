using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServices.OrderService.Models.Domain
{
    public class Order
    {
        public Order()
        {
           
        }

        public Order(string identifier)
        {
            Identifier = identifier;
        }

        public Order(string identifier, string customer)
        {
            Identifier = identifier;
            Customer = customer;
        }

        [Key]
        public string Identifier { get;  set; }
        public string Customer { get;  set; }

        public ICollection<OrderLine> Items { get;  set; }
                = new List<OrderLine>();

    }
}
