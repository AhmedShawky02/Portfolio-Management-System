using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP.NET_Web_API_Project.Helpers
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _Key;
        private readonly UserManager<AppUser> _userManager; // إضافة UserManager

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!));
            _userManager = userManager; // حقن UserManager
        }

        public async Task<string> CreateToken(AppUser user)
        {
            // إضافة الـ claims الأساسية
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
            };

            // جلب الأدوار الخاصة بالمستخدم
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // إضافة الأدوار كـ claims
            }

            // إنشاء SigningCredentials
            var creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);

            // إعدادات التوكين
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            // إنشاء التوكين
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
