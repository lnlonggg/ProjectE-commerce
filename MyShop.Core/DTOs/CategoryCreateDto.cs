namespace MyShop.Core.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int OrderIndex { get; set; }
    }
}