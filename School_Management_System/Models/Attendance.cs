using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public enum AttendanceStatus
    {
        Present = 1,
        Absent,
        Late,
        Excused
    }
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ClassID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present; // 'Present', 'Absent', 'Late', 'Excused'

        // Navigation properties
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }
    }
}
