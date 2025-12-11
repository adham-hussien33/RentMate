using Microsoft.EntityFrameworkCore;
using RentMate.API.Data;
using RentMate.API.DTOs.Admin;
using RentMate.API.Models;

namespace RentMate.API.Services
{
    public class AdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        // List pending landlords
        public async Task<List<Landlord>> GetPendingLandlordsAsync()
        {
            return await _context.Users.OfType<Landlord>().Where(l => l.Role == "Landlord" && l.Properties.Count == 0).ToListAsync();
        }

        // Approve/reject landlord
        public async Task ApproveLandlordAsync(LandlordApprovalDto dto)
        {
            var landlord = await _context.Users.OfType<Landlord>().FirstOrDefaultAsync(l => l.Id == dto.LandlordId);
            if (landlord == null) throw new Exception("Landlord not found");
            landlord.Role = dto.Status == "Approved" ? "Landlord" : "Rejected";
            await _context.SaveChangesAsync();
        }

        // List pending property posts
        public async Task<List<PropertyPost>> GetPendingPropertiesAsync()
        {
            return await _context.PropertyPosts.Where(p => p.Status == "Pending").ToListAsync();
        }

        // Approve/reject property post
        public async Task ApprovePropertyAsync(PropertyApprovalDto dto)
        {
            var property = await _context.PropertyPosts.FirstOrDefaultAsync(p => p.Id == dto.PropertyPostId);
            if (property == null) throw new Exception("Property not found");
            property.Status = dto.Status;
            await _context.SaveChangesAsync();
        }
    }
} 