namespace GymManagementSystem.Services.Interfaces;

public interface IEnrollmentService
{
    Task<(bool Success, string Message)> EnrollAsync(int memberId, int gymClassId, DateTime date);
}