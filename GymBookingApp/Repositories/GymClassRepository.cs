using GymBookingApp.Data;
using GymBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBookingApp.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext _context;

        public GymClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GymClass>> GetAllAsync(int id)
        {
            var gymClasses = await _context.GymClasses
                 .Include(g => g.ApplicationUsers)
                 .Where(g => g.StartTime > DateTime.UtcNow)
                 .ToListAsync();
            return gymClasses;
        }
    }
}
