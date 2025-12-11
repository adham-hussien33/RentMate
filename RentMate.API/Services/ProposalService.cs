using Microsoft.EntityFrameworkCore;
using RentMate.API.Data;
using RentMate.API.DTOs.Proposal;
using RentMate.API.Models;

namespace RentMate.API.Services
{
    public class ProposalService
    {
        private readonly ApplicationDbContext _context;
        public ProposalService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tenant: Submit proposal
        public async Task<ProposalResponseDto> CreateProposalAsync(int tenantId, ProposalCreateDto dto)
        {
            var tenant = await _context.Users.OfType<Tenant>().FirstOrDefaultAsync(t => t.Id == tenantId);
            var property = await _context.PropertyPosts.FirstOrDefaultAsync(p => p.Id == dto.PropertyPostId);
            if (tenant == null || property == null) throw new Exception("Tenant or property not found");
            var proposal = new RentalProposal
            {
                PropertyPostId = dto.PropertyPostId,
                TenantId = tenantId,
                Documents = dto.Documents,
                ApprovalStatus = "Pending"
            };
            _context.RentalProposals.Add(proposal);
            await _context.SaveChangesAsync();
            return await ToResponseDto(proposal.Id);
        }

        // Tenant: View own proposals
        public async Task<List<ProposalResponseDto>> GetProposalsByTenantAsync(int tenantId)
        {
            var proposals = await _context.RentalProposals
                .Include(rp => rp.PropertyPost)
                .Where(rp => rp.TenantId == tenantId)
                .ToListAsync();
            return (await Task.WhenAll(proposals.Select(p => ToResponseDto(p.Id)))).ToList();
        }

        // Landlord: View proposals for their properties
        public async Task<List<ProposalResponseDto>> GetProposalsForLandlordAsync(int landlordId)
        {
            var proposals = await _context.RentalProposals
                .Include(rp => rp.PropertyPost)
                .Where(rp => rp.PropertyPost.LandlordId == landlordId)
                .ToListAsync();
            return (await Task.WhenAll(proposals.Select(p => ToResponseDto(p.Id)))).ToList();
        }

        // Landlord: Update proposal status
        public async Task<ProposalResponseDto> UpdateProposalStatusAsync(int proposalId, int landlordId, ProposalStatusUpdateDto dto)
        {
            var proposal = await _context.RentalProposals.Include(rp => rp.PropertyPost).FirstOrDefaultAsync(rp => rp.Id == proposalId);
            if (proposal == null || proposal.PropertyPost.LandlordId != landlordId)
                throw new Exception("Proposal not found or access denied");
            proposal.ApprovalStatus = dto.ApprovalStatus;
            await _context.SaveChangesAsync();
            return await ToResponseDto(proposal.Id);
        }

        private async Task<ProposalResponseDto> ToResponseDto(int proposalId)
        {
            var proposal = await _context.RentalProposals
                .Include(rp => rp.PropertyPost)
                .Include(rp => rp.Tenant)
                .FirstOrDefaultAsync(rp => rp.Id == proposalId);
            return new ProposalResponseDto
            {
                Id = proposal.Id,
                PropertyPostId = proposal.PropertyPostId,
                PropertyTitle = proposal.PropertyPost?.Title ?? "",
                TenantId = proposal.TenantId,
                TenantName = proposal.Tenant?.Name ?? "",
                Documents = proposal.Documents,
                ApprovalStatus = proposal.ApprovalStatus
            };
        }
    }
} 