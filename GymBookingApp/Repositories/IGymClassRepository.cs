using GymBookingApp.Models;
namespace GymBookingApp.Repositories;
public interface IGymClassRepository
{
    Task<IEnumerable<GymClass>> GetAllAsync();
    Task<GymClass> GetByIdAsync(int? id);

    void Add(GymClass gymClass);
    void Update(GymClass gymClass);
    void Remove(GymClass gymClass);
    Task CompleteAsyncTask();
}
