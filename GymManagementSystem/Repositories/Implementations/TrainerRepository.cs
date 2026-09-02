using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories.Implementations;

public class TrainerRepository: ITrainerRepository
{
    private readonly ApplicationDbContext _context;
    public TrainerRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Trainer>> GetAllAsync() =>
        await _context.Trainers.ToListAsync();

    public async Task<Trainer> GetByIdAsync(int id) =>
        await _context.Trainers.FindAsync(id);

    public async Task AddAsync(Trainer trainer) =>
        await _context.Trainers.AddAsync(trainer);

    public void Update(Trainer trainer) => _context.Trainers.Update(trainer);

    public void Delete(Trainer trainer) => _context.Trainers.Remove(trainer);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}