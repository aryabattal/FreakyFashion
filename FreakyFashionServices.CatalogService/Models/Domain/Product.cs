using FreakyFashionServices.CatalogService.Extensions;
using System.ComponentModel.DataAnnotations;

namespace FreakyFashionServices.CatalogService.Models.Domain
{
    public class Product
    {
        public Product(string articleNumber, string name, string description, Uri imageUrl, int price)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            UrlSlug = name.Slugify(); 
         
        }

        [Key]
        public string ArticleNumber { get; set; }
   
        public string Name{ get; set; }
        public string Description { get; set; }
        public Uri ImageUrl { get; set; }
        public int Price { get; set; }
        public string UrlSlug { get; set; }
        public int Id { get; set; }


    }
}
