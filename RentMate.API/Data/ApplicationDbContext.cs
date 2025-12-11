using Microsoft.EntityFrameworkCore;
using RentMate.API.Models;

namespace RentMate.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PropertyPost> PropertyPosts { get; set; }
        public DbSet<RentalProposal> RentalProposals { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>().HasDiscriminator().HasValue("Admin");
            modelBuilder.Entity<Landlord>().HasDiscriminator().HasValue("Landlord");
            modelBuilder.Entity<Tenant>().HasDiscriminator().HasValue("Tenant");

            modelBuilder.Entity<RentalProposal>()
                .HasOne(rp => rp.Tenant)
                .WithMany(t => t.Proposals)
                .HasForeignKey(rp => rp.TenantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalProposal>()
                .HasOne(rp => rp.PropertyPost)
                .WithMany(p => p.Proposals)
                .HasForeignKey(rp => rp.PropertyPostId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 