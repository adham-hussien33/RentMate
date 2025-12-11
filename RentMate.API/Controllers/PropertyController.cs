using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentMate.API.DTOs.Property;
using RentMate.API.Services;
using System.Security.Claims;

namespace RentMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _propertyService;
        public PropertyController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        // Landlord: Create property
        [HttpPost]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Create(PropertyCreateDto dto)
        {
            var landlordId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var result = await _propertyService.CreatePropertyAsync(landlordId, dto);
            return Ok(result);
        }

        // Landlord: Update property
        [HttpPut("{id}")]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Update(int id, PropertyUpdateDto dto)
        {
            var landlordId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            var result = await _propertyService.UpdatePropertyAsync(id, landlordId, dto);
            return Ok(result);
        }

        // Landlord: Delete property
        [HttpDelete("{id}")]
        [Authorize(Roles = "Landlord")]
        public async Task<IActionResult> Delete(int id)
        {
            var landlordId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            await _propertyService.DeletePropertyAsync(id, landlordId);
            return NoContent();
        }

        // Public: Get all properties
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _propertyService.GetAllPropertiesAsync();
            return Ok(result);
        }

        // Public: Get property by id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _propertyService.GetPropertyByIdAsync(id);
            return Ok(result);
        }
    }
} 