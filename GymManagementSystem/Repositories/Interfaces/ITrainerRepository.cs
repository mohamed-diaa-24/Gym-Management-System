using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces;

public interface ITrainerRepository
{
    
    Task<IEnumerable<Trainer>> GetAllAsync();
    Task<Trainer> GetByIdAsync(int id);
    Task AddAsync(Trainer trainer);
    void Update(Trainer trainer);
    void Delete(Trainer trainer);
    Task SaveAsync();
}