using GymManagementSystem.Models;

namespace GymManagementSystem.Services.Interfaces;

public interface IGymClassService
{
    Task<IEnumerable<GymClass>> GetAllAsync();
    Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId);
    Task<GymClass> GetByIdAsync(int id);
    Task<GymClass> GetWithDetailsAsync(int id);
    Task CreateAsync(GymClass gymClass);
    Task UpdateAsync(GymClass gymClass);
    Task DeleteAsync(int id);
}