using GymManagementSystem.Models;

namespace GymManagementSystem.Services.Interfaces;

public interface ITrainerService
{
    
    Task<IEnumerable<Trainer>> GetAllAsync();
    Task<Trainer> GetByIdAsync(int id);
    Task CreateAsync(Trainer trainer);
    Task UpdateAsync(Trainer trainer);
    Task DeleteAsync(int id);
}