using Microsoft.AspNetCore.Identity;

namespace GymBookingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string FullName => FirstName + " " + LastName;

        public DateTime TimeOfRegistration { get; set; }

        // Navigation property
        public ICollection<ApplicationUserGymClass>? GymClasses { get; set; }
    }
}
