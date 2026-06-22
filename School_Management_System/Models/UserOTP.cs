using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class UserOTP
    {
        public int Id { get; set; }
        [Required]
        public string OTP { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public string UserID { get; set; }
        public User User { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        public DateTime ValidTo { get; set; } = DateTime.Now.AddHours(3);
        public bool IsUsed { get; set; } = false;

    }
}
