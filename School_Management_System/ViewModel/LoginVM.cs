using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace School_Management_System.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "User Name OR Email ")]
        [Required]
        public string EmailOrUserName { get; set; }=string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Pssword { get; set; }= string.Empty;
        public bool IsPersistent { get; set; }=false;

    }
}
