namespace RentMate.API.Models
{
    public class RentalProposal
    {
        public int Id { get; set; }
        public int PropertyPostId { get; set; }
        public PropertyPost PropertyPost { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Documents { get; set; } // File paths or JSON
        public string ApprovalStatus { get; set; } // Pending, Approved, Rejected
    }
} 