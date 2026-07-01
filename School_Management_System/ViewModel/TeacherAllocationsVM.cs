using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.ViewModel
{
    public class TeacherAllocationsVM
    {
        public  Teacher Teacher { get; set; }
        public  Class  Class { get; set; }
        public  Subject Subject { get; set; }
    }
}
