using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Exam
    {
        [Key]
        public int ExamID { get; set; }

        [Required]
        [StringLength(50)]
        public string ExamName { get; set; }

        [Required]
        [StringLength(20)]
        public string Term { get; set; }

        public  IEnumerable<Mark> Marks { get; set; }
    }
}
