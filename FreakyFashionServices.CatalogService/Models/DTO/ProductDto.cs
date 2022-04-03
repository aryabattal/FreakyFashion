using FreakyFashionServices.CatalogService.Extensions;

namespace FreakyFashionServices.CatalogService.Models.DTO
{
    public class ProductDto
    {
        public ProductDto(string articleNumber, string name, string description, Uri imageUrl, int price, string urlSlug, int id)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            UrlSlug = name.Slugify();
            Id = id;
        }

        public string ArticleNumber { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Uri ImageUrl { get; set; }
        public int Price { get; set; }
        public string UrlSlug { get; set; }
        public int Id { get; set; }
    }
}