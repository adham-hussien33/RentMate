namespace RentMate.API.DTOs.Admin
{
    public class PropertyApprovalDto
    {
        public int PropertyPostId { get; set; }
        public string Status { get; set; } // Approved, Rejected
    }
} 