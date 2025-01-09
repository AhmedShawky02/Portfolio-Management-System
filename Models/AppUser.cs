using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Web_API_Project.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolio { get; set; } = new List<Portfolio>();
    }
}
