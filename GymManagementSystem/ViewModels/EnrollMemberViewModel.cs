using System.ComponentModel.DataAnnotations;
using GymManagementSystem.Models;

namespace GymManagementSystem.ViewModels;

public class EnrollMemberViewModel
{
    [Required]
    public int MemberId { get; set; }

    [Required]
    public int GymClassId { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; } = DateTime.Today;

    public IEnumerable<Member> Members { get; set; }
    public IEnumerable<GymClass> GymClasses { get; set; }
}