using Microsoft.EntityFrameworkCore;
using RentMate.API.Data;
using RentMate.API.DTOs.Property;
using RentMate.API.Models;

namespace RentMate.API.Services
{
    public class PropertyService
    {
        private readonly ApplicationDbContext _context;
        public PropertyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PropertyResponseDto> CreatePropertyAsync(int landlordId, PropertyCreateDto dto)
        {
            var landlord = await _context.Users.OfType<Landlord>().FirstOrDefaultAsync(l => l.Id == landlordId);
            if (landlord == null) throw new Exception("Landlord not found");
            var property = new PropertyPost
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Location = dto.Location,
                Images = dto.Images,
                Status = "Pending",
                LandlordId = landlordId
            };
            _context.PropertyPosts.Add(property);
            await _context.SaveChangesAsync();
            return ToResponseDto(property, landlord.Name);
        }

        public async Task<PropertyResponseDto> UpdatePropertyAsync(int propertyId, int landlordId, PropertyUpdateDto dto)
        {
            var property = await _context.PropertyPosts.FirstOrDefaultAsync(p => p.Id == propertyId && p.LandlordId == landlordId);
            if (property == null) throw new Exception("Property not found or access denied");
            property.Title = dto.Title;
            property.Description = dto.Description;
            property.Price = dto.Price;
            property.Location = dto.Location;
            property.Images = dto.Images;
            property.Status = dto.Status;
            await _context.SaveChangesAsync();
            var landlord = await _context.Users.OfType<Landlord>().FirstOrDefaultAsync(l => l.Id == landlordId);
            return ToResponseDto(property, landlord?.Name ?? "");
        }

        public async Task DeletePropertyAsync(int propertyId, int landlordId)
        {
            var property = await _context.PropertyPosts.FirstOrDefaultAsync(p => p.Id == propertyId && p.LandlordId == landlordId);
            if (property == null) throw new Exception("Property not found or access denied");
            _context.PropertyPosts.Remove(property);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PropertyResponseDto>> GetAllPropertiesAsync()
        {
            var properties = await _context.PropertyPosts.Include(p => p.Landlord).ToListAsync();
            return properties.Select(p => ToResponseDto(p, p.Landlord?.Name ?? "")).ToList();
        }

        public async Task<PropertyResponseDto> GetPropertyByIdAsync(int id)
        {
            var property = await _context.PropertyPosts.Include(p => p.Landlord).FirstOrDefaultAsync(p => p.Id == id);
            if (property == null) throw new Exception("Property not found");
            return ToResponseDto(property, property.Landlord?.Name ?? "");
        }

        private PropertyResponseDto ToResponseDto(PropertyPost p, string landlordName) => new PropertyResponseDto
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            Location = p.Location,
            Images = p.Images,
            Status = p.Status,
            LandlordId = p.LandlordId,
            LandlordName = landlordName
        };
    }
} 