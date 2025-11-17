namespace MyShop.Core.DTOs
{
    public class GroupCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
    }
}