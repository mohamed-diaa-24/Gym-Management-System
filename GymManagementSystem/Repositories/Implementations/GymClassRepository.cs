using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories.Implementations;

public class GymClassRepository : IGymClassRepository
{
    private readonly ApplicationDbContext _context;
    public GymClassRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<GymClass>> GetAllAsync() =>
        await _context.GymClasses.Include(c => c.Trainer).ToListAsync();

    public async Task<IEnumerable<GymClass>> GetByTrainerIdAsync(int trainerId) =>
        await _context.GymClasses.Include(c => c.Trainer)
            .Where(c => c.TrainerId == trainerId).ToListAsync();

    public async Task<GymClass> GetByIdAsync(int id) =>
        await _context.GymClasses.FindAsync(id);

    public async Task<GymClass> GetWithDetailsAsync(int id) =>
        await _context.GymClasses
            .Include(c => c.Trainer)
            .Include(c => c.Enrollments).ThenInclude(e => e.Member)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(GymClass gymClass) =>
        await _context.GymClasses.AddAsync(gymClass);

    public void Update(GymClass gymClass) => _context.GymClasses.Update(gymClass);

    public void Delete(GymClass gymClass) => _context.GymClasses.Remove(gymClass);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}