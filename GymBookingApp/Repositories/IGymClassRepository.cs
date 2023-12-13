using GymBookingApp.Models;
using GymBookingApp.Models.ViewModels;

namespace GymBookingApp.Repositories
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAllAsync();
    }
}