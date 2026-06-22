using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class ConfirmOtpVM
    {
        [Required]
         public string? OtpNumber { get; set; }
    }
}
