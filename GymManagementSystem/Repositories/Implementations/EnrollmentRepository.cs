using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories.Implementations;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;
    public EnrollmentRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Enrollment>> GetAllAsync() =>
        await _context.Enrollments
            .Include(e => e.Member).Include(e => e.GymClass).ToListAsync();

    public async Task<bool> ExistsAsync(int memberId, int gymClassId) =>
        await _context.Enrollments.AnyAsync(e => e.MemberId == memberId && e.GymClassId == gymClassId);

    public async Task AddAsync(Enrollment enrollment) =>
        await _context.Enrollments.AddAsync(enrollment);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}