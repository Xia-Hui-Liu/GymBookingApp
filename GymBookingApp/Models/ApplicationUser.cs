using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace GymBookingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property
        public ICollection<ApplicationUserGymClass>? GymClasses { get; set; }
    }

        
}
