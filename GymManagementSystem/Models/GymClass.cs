using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GymManagementSystem.Models
{
    public class GymClass
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(500)]
        public string Description { get; set; }

        [Required, StringLength(100)]
        public string Schedule { get; set; }

        [ForeignKey(nameof(Trainer))]
        public int TrainerId { get; set; }
        [ValidateNever]
        public Trainer Trainer { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}