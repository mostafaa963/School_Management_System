using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class UpdateSubjectVM
    {
        public int SubjectID { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Subject Name Must be 3")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Subject Code Must be 3")]
        //[Display(Name = "Subject Code")]
        [Remote("CheckName", "UniqueName", "Admin",ErrorMessage = "Must be Unique")]
        public string SubjectCode { get; set; }
    }
}
