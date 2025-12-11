using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMate.API.DTOs.Admin;
using RentMate.API.Services;

namespace RentMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("pending-landlords")]
        public async Task<IActionResult> GetPendingLandlords()
        {
            var result = await _adminService.GetPendingLandlordsAsync();
            return Ok(result);
        }

        [HttpPost("approve-landlord")]
        public async Task<IActionResult> ApproveLandlord(LandlordApprovalDto dto)
        {
            await _adminService.ApproveLandlordAsync(dto);
            return Ok();
        }

        [HttpGet("pending-properties")]
        public async Task<IActionResult> GetPendingProperties()
        {
            var result = await _adminService.GetPendingPropertiesAsync();
            return Ok(result);
        }

        [HttpPost("approve-property")]
        public async Task<IActionResult> ApproveProperty(PropertyApprovalDto dto)
        {
            await _adminService.ApprovePropertyAsync(dto);
            return Ok();
        }
    }
} 