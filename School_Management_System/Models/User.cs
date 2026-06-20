using Microsoft.AspNetCore.Identity;

namespace School_Management_System.Models
{
    public class User : IdentityUser
    {

        public string  FullName { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
