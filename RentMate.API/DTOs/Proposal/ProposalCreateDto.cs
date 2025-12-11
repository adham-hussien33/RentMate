namespace RentMate.API.DTOs.Proposal
{
    public class ProposalCreateDto
    {
        public int PropertyPostId { get; set; }
        public string Documents { get; set; } // Optional
    }
} 