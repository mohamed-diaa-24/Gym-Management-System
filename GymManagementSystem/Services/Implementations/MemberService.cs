using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.Services.Interfaces;

namespace GymManagementSystem.Services.Implementations;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repo;
    public MemberService(IMemberRepository repo) => _repo = repo;

    public Task<IEnumerable<Member>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Member> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

    public async Task CreateAsync(Member member)
    {
        await _repo.AddAsync(member);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(Member member)
    {
        _repo.Update(member);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var member = await _repo.GetByIdAsync(id);
        if (member != null)
        {
            _repo.Delete(member);
            await _repo.SaveAsync();
        }
    }
}