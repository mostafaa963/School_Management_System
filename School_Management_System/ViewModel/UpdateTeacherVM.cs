using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class UpdateTeacherVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Qualification { get; set; }

        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
