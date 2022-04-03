﻿namespace FreakyFashionServices.BasketService.Models.DTO
{
    public class BasketDto
    {
        

        public string Identifier { get; set; }
     
        public ICollection<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }



}