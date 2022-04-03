using FreakyFashionServices.OrderService.Models.DTO.BasketService;

namespace FreakyFashionServices.Models.Domain.DTO
{
    public class BasketDto
    {
        public string Identifier { get; set; }
     
        public ICollection<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

    }



}