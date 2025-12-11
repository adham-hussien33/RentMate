using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMate.API.DTOs.Proposal;
using RentMate.API.Services;
using System.Security.Claims;

namespace RentMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProposalController : ControllerBase
    {
        private readonly ProposalService _proposalService;
        public ProposalController(ProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        // Tenant: Submit proposal
        [HttpPost]
        [Authorize(Roles = "Tenant")]
        public async Task<IActionResult> Create(ProposalCreateDto dto)
        {
            var tenantId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var result = await _proposalService.CreateProposalAsync(tenantId, dto);
            return Ok(result);
        }

        // Tenant: View own proposals
        [HttpGet("my")] 
        [Authorize(Roles = "Tenant")]
        public async Task<IActionResult> GetMyProposals()
        {
            var tenantId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var result = await _proposalService.GetProposalsByTenantAsync(tenantId);
            return Ok(result);
        }

        // Landlord: View proposals for their properties
        [HttpGet("for-landlord")] 
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> GetProposalsForLandlord()
        {
            var landlordId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var result = await _proposalService.GetProposalsForLandlordAsync(landlordId);
            return Ok(result);
        }

        // Landlord: Update proposal status
        [HttpPut("{id}/status")] 
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> UpdateStatus(int id, ProposalStatusUpdateDto dto)
        {
            var landlordId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var result = await _proposalService.UpdateProposalStatusAsync(id, landlordId, dto);
            return Ok(result);
        }
    }
} 