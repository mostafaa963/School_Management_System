using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace School_Management_System.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Qualification { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User UserProfile { get; set; }
        public virtual ICollection<TeacherAllocation> Allocations { get; set; }
    }
}
