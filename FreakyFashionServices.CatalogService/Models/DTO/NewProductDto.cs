namespace FreakyFashionServices.CatalogService.Models.DTO
{
    public class NewProductDto
    {
      
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri ImageUrl { get; set; }
        public int Price { get; set; }
    }
}