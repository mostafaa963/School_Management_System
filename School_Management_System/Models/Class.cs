using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassName { get; set; }

        [Required]
        [StringLength(10)]
        public string Section { get; set; }

        public int Capacity { get; set; } = 40;

        // Navigation properties
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TeacherAllocation> Allocations { get; set; }
    }
}
