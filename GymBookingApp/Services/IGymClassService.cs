using GymBookingApp.Models.ViewModels;

namespace GymBookingApp.Services
{
    public interface IGymClassService
    {
        Task<IEnumerable<IndexViewModel>> GetAllAsync();
    }
}