using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMate.API.Data;
using RentMate.API.Models;

namespace RentMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get messages between two users
        [HttpGet("between/{userId1}/{userId2}")]
        public async Task<IActionResult> GetMessagesBetween(int userId1, int userId2)
        {
            var messages = await _context.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
            return Ok(messages);
        }

        // Get messages for a property post (comment thread)
        [HttpGet("property/{propertyPostId}")]
        public async Task<IActionResult> GetMessagesForProperty(int propertyPostId)
        {
            var messages = await _context.Messages
                .Where(m => m.PropertyPostId == propertyPostId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
            return Ok(messages);
        }
    }
} 