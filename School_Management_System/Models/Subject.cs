using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [Required]
        [StringLength(100)]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(20)]
        public string SubjectCode { get; set; }

        // Navigation properties
        public  IEnumerable<TeacherAllocation> Allocations { get; set; }
    }
}
