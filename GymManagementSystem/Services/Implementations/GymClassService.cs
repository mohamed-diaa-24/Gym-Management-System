using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.Services.Interfaces;

namespace GymManagementSystem.Services.Implementations;

public class GymClassService : IGymClassService
{
    private readonly IGymClassRepository _repo;
    public GymClassService(IGymClassRepository repo) => _repo = repo;

    public Task<IEnumerable<GymClass>> GetAllAsync() => _repo.GetAllAsync();
    public Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId) => _repo.GetByTrainerIdAsync(trainerId);
    public Task<GymClass> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<GymClass> GetWithDetailsAsync(int id) => _repo.GetWithDetailsAsync(id);

    public async Task CreateAsync(GymClass gymClass)
    {
        await _repo.AddAsync(gymClass);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(GymClass gymClass)
    {
        _repo.Update(gymClass);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var gymClass = await _repo.GetByIdAsync(id);
        if (gymClass != null)
        {
            _repo.Delete(gymClass);
            await _repo.SaveAsync();
        }
    }
}