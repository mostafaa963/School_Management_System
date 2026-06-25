using System.ComponentModel.DataAnnotations;

namespace School_Management_System.ViewModel
{
    public class ClassDetailsVM
    {
        public int Id { get; set; }
        public string ClassName { get; set; }

        [Required]

        public string Section { get; set; }
        [Required]
        public int Capacity { get; set; } = 40;
        public int Count { get; set; }
    }
}
