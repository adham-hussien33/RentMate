using Microsoft.AspNetCore.SignalR;
using RentMate.API.Data;
using RentMate.API.Models;

namespace RentMate.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        // Send a message from one user to another (optionally for a property post)
        public async Task SendMessage(int senderId, int receiverId, string content, int? propertyPostId = null)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow,
                PropertyPostId = propertyPostId
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
        }
    }
} 