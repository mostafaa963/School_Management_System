using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class NewPasswordVM
    {
        [Required]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

    }
}
