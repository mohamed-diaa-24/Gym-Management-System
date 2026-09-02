using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.Services.Interfaces;

namespace GymManagementSystem.Services.Implementations;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _repo;
    public TrainerService(ITrainerRepository repo) => _repo = repo;

    public Task<IEnumerable<Trainer>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Trainer> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(Trainer trainer)
    {
        await _repo.AddAsync(trainer);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(Trainer trainer)
    {
        _repo.Update(trainer);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var trainer = await _repo.GetByIdAsync(id);
        if (trainer != null)
        {
            _repo.Delete(trainer);
            await _repo.SaveAsync();
        }
    }
}