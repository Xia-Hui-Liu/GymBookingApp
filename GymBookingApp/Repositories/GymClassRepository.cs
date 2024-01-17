using GymBookingApp.Data;
using GymBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GymBookingApp.Repositories;

public class GymClassRepository : IGymClassRepository
{
    private readonly ApplicationDbContext _context;

    public GymClassRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GymClass>> GetAllAsync()
    {
        var gymClasses = await _context.GymClasses
             .Include(g => g.ApplicationUsers)
             .Where(g => g.StartTime > DateTime.UtcNow)
             .ToListAsync();
        return gymClasses;
    }

    public async Task<GymClass> GetByIdAsync(int? id)
    {
        var gymClassWithAttendees = await _context.GymClasses
            .Include(c => c.ApplicationUsers)
                .ThenInclude(u => u.ApplicationUser)
            .FirstOrDefaultAsync(g => g.Id == id);

        return gymClassWithAttendees;
    }

    public void Add(GymClass gymClass)
    {
        _context.GymClasses.Add(gymClass);
    }

    public void Update(GymClass gymClass)
    {
        _context.GymClasses.Update(gymClass);
    }

    public void Remove(GymClass gymClass)
    {
        // Mark the entire entity as deleted
        _context.Entry(gymClass).State = EntityState.Deleted;

    }
    public async Task CompleteAsyncTask()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw;
        }
    }
}
