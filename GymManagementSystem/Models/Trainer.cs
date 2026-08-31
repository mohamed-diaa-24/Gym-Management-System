using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Specialization { get; set; }

        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
    }
}