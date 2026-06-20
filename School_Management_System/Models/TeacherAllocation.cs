using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class TeacherAllocation
    {
        [Key]
        public int AllocationID { get; set; }

        [Required]
        public int TeacherID { get; set; }

        [Required]
        public int ClassID { get; set; }

        [Required]
        public int SubjectID { get; set; }

        // Navigation properties
        [ForeignKey("TeacherID")]
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }
    }
}
