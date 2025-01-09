using ASP.NET_Web_API_Project.Models;

namespace ASP.NET_Web_API_Project.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
