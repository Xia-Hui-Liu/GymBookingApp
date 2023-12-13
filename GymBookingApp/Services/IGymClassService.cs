using GymBookingApp.Models;
using GymBookingApp.Models.ViewModels;

namespace GymBookingApp.Services
{
    public interface IGymClassService
    {
        Task<IndexViewModel> GetAllAsync();
    }
}