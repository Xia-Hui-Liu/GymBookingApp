

using Microsoft.AspNetCore.Identity;


namespace GymBookingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Navigation property
        public ICollection<ApplicationUserGymClass>? GymClasses { get; set; }
    }

        
}
