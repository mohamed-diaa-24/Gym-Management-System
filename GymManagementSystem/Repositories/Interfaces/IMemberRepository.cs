using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member> GetByIdAsync(int id);
    Task AddAsync(Member member);
    void Update(Member member);
    void Delete(Member member);
    Task SaveAsync();
}