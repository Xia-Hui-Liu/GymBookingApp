using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace GymBookingApp.Models
{
    public class ApplicationUserGymClass
    {
     
        // Foreign key for ApplicationUser
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        // Foreign key for GymClass
        public int? GymClassId { get; set; }
        public GymClass? GymClass { get; set; }
    }

    public class ApplicationUserGymClassConfiguration : IEntityTypeConfiguration<ApplicationUserGymClass>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserGymClass> builder)
        {
            // Configure the relationship with ApplicationUser and GymClass

            builder
                .HasKey(k => new { k.ApplicationUserId, k.GymClassId });
            builder
                .HasOne(agc => agc.ApplicationUser)
                .WithMany(u => u!.GymClasses)
                .HasForeignKey(agc => agc.ApplicationUserId);

            builder
                .HasOne(agc => agc.GymClass)
                .WithMany(g => g!.ApplicationUsers)
                .HasForeignKey(agc => agc.GymClassId);

        }
    }
}
