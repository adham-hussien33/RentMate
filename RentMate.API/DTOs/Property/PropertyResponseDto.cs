namespace RentMate.API.DTOs.Property
{
    public class PropertyResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Images { get; set; }
        public string Status { get; set; }
        public int LandlordId { get; set; }
        public string LandlordName { get; set; }
    }
} 