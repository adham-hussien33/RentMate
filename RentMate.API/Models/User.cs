namespace RentMate.API.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // "Admin", "Landlord", "Tenant"
    }

    public class Admin : User { }

    public class Landlord : User
    {
        public ICollection<PropertyPost> Properties { get; set; }
    }

    public class Tenant : User
    {
        public ICollection<RentalProposal> Proposals { get; set; }
    }
} 