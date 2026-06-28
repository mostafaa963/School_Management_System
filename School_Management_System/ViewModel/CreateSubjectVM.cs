using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class CreateSubjectVM
    {
        [Required]
        [Display (Name = "Subject Name ")]
        public string SubjectName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        [Display (Name = "Subject Code")]
        public string SubjectCode { get; set; }
    }
}
