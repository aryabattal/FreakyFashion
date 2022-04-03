namespace FreakyFashionServices.CatalogService.Extensions
{
    public static class StringExtenions
    {
        public static string Slugify(this string name) =>
            name.Trim()
                .Replace("-", "")
                .Replace(" ", "-")
                .ToLower();
    }
}
