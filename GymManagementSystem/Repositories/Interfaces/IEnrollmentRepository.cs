using GymManagementSystem.Models;

namespace GymManagementSystem.Repositories.Interfaces;

public interface IEnrollmentRepository
{
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<bool> ExistsAsync(int memberId, int gymClassId);
    Task AddAsync(Enrollment enrollment);
    Task SaveAsync();
}