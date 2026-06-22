using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class ForgetPasswordVM
    {
        [Required]
        [Display(Name ="Email Or UserName")]
        public string? EmailOrUserName { get; set; }
    }
}
