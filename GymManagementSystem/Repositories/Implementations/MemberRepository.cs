using GymManagementSystem.Data;
using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Repositories.Implementations;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext _context;
    public MemberRepository(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<Member>> GetAllAsync() =>
        await _context.Members.ToListAsync();

    public async Task<Member> GetByIdAsync(int id) =>
        await _context.Members.FindAsync(id);

    public async Task AddAsync(Member member) =>
        await _context.Members.AddAsync(member);

    public void Update(Member member) => _context.Members.Update(member);

    public void Delete(Member member) => _context.Members.Remove(member);

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}