namespace RentMate.API.DTOs.Admin
{
    public class LandlordApprovalDto
    {
        public int LandlordId { get; set; }
        public string Status { get; set; } // Approved, Rejected
    }
} 