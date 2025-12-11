namespace RentMate.API.DTOs.Property
{
    public class PropertyUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Images { get; set; }
        public string Status { get; set; }
    }
} 