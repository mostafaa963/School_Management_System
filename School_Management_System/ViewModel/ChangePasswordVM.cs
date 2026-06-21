using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class ChangePasswordVM
    {

        [Required]
        [Display(Name ="Email Or UserName")]
        public string? EmailOrUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string? ConfirmNewPassword { get; set; }
    }
}
