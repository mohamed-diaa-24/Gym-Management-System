using System.ComponentModel.DataAnnotations;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GymManagementSystem.ViewModels;

public class EnrollMemberViewModel
{
    [Required]
    public int MemberId { get; set; }

    [Required]
    public int GymClassId { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; } = DateTime.Today;

    [ValidateNever]
    public IEnumerable<Member> Members { get; set; }

    [ValidateNever]
    public IEnumerable<GymClass> GymClasses { get; set; }
}