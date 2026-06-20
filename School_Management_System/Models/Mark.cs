using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Mark
    {
        [Key]
        public int MarkID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int SubjectID { get; set; }

        [Required]
        public int ExamID { get; set; }

        [Required]
        public decimal TotalMarks { get; set; }

        [Required]
        public decimal ObtainedMarks { get; set; }

        // Navigation properties
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubjectID")]
        public virtual Subject Subject { get; set; }

        [ForeignKey("ExamID")]
        public virtual Exam Exam { get; set; }
    }
}
