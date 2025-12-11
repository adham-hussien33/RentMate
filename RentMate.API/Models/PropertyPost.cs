namespace RentMate.API.Models
{
    public class PropertyPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Images { get; set; } // Comma-separated or JSON
        public string Status { get; set; } // e.g., Pending, Approved, Rejected
        public int LandlordId { get; set; }
        public Landlord Landlord { get; set; }
        public ICollection<RentalProposal> Proposals { get; set; }
    }
} 