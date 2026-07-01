using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class TeacherIndexVM
    {
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name ="Hire date")]
        [DataType (DataType.DateTime)]
        public DateTime HireDate { get; set; }
        public virtual IEnumerable<TeacherAllocation> Allocations { get; set; }
    }
}
