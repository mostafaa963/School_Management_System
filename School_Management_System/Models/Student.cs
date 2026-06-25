using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        public string? UserID { get; set; } // Nullable if login account isn't required immediately

      

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [StringLength(20)]
        public string ParentContact { get; set; }

        public int? ClassID { get; set; } // Nullable to handle ON DELETE SET NULL constraint

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual User UserProfile { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class StudentClass { get; set; }
    }
}
