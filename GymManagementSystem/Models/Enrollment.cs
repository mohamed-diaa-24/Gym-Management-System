using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey(nameof(GymClass))]
        public int GymClassId { get; set; }
        public GymClass GymClass { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}