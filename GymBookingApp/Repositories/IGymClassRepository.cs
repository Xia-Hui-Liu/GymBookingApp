using GymBookingApp.Models;

namespace GymBookingApp.Repositories
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAllAsync(int id);
    }
}