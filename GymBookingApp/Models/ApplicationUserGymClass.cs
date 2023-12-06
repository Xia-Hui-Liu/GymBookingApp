using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymBookingApp.Models
{
    public class ApplicationUserGymClass
    {
        // Primary key for the ApplicationUserGymClass table
        public int Id { get; set; }

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
            // Configure the many-to-many relationship with ApplicationUser and GymClass
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
