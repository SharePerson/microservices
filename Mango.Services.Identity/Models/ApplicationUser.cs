using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
    }
}
