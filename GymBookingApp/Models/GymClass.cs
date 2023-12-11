using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GymBookingApp.Models
{
    public class GymClass
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get { return StartTime + Duration; } }
        public string? Description { get; set; }

        // Navigation property
        public ICollection<ApplicationUserGymClass>? ApplicationUsers { get; set; } = new List<ApplicationUserGymClass>();
    }


    public class GymClassConfiguration : IEntityTypeConfiguration<GymClass>
    {
        public void Configure(EntityTypeBuilder<GymClass> builder)
        {
            // Configure primary key
            builder.HasKey(g => g.Id);

            // Configure properties
            builder.Property(g => g.Name).HasMaxLength(255); 
            builder.Property(g => g.Description).HasMaxLength(1000);

            
        }
    }
}