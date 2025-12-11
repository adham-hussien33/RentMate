namespace RentMate.API.DTOs.Proposal
{
    public class ProposalResponseDto
    {
        public int Id { get; set; }
        public int PropertyPostId { get; set; }
        public string PropertyTitle { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string Documents { get; set; }
        public string ApprovalStatus { get; set; }
    }
} 