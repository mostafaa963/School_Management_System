using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class CreateClassVM
    {
        [Required]
        public string ClassName { get; set; }

        [Required]
       
        public string Section { get; set; }
        [Required]
        public int Capacity { get; set; } = 40;
        
    }
}
