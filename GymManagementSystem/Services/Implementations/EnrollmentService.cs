using GymManagementSystem.Models;
using GymManagementSystem.Repositories.Interfaces;
using GymManagementSystem.Services.Interfaces;

namespace GymManagementSystem.Services.Implementations;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo;
    public EnrollmentService(IEnrollmentRepository repo) => _repo = repo;

    public async Task<(bool, string)> EnrollAsync(int memberId, int gymClassId, DateTime date)
    {
        if (await _repo.ExistsAsync(memberId, gymClassId))
            return (false, "This member is already enrolled in this class.");

        await _repo.AddAsync(new Enrollment
        {
            MemberId = memberId,
            GymClassId = gymClassId,
            EnrollmentDate = date
        });
        await _repo.SaveAsync();
        return (true, "Member enrolled successfully.");
    }
}