using Microsoft.AspNetCore.Identity;

namespace TP5.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}