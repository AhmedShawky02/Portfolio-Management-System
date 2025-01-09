using ASP.NET_Web_API_Project.DTOs.Account;
using ASP.NET_Web_API_Project.Interfaces;
using ASP.NET_Web_API_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace ASP.NET_Web_API_Project.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) 
            {
                return Unauthorized("Username not found and/or password incorrect");
            }

            return Ok
            (
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user)
                }
            );

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser()
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var CreatedUser = await _userManager.CreateAsync(appUser,registerDto.Password);

                if(CreatedUser.Succeeded)
                {
                    //// قائمة الأدوار
                    //var roles = new List<string> { "Admin", "User" };

                    //// إضافة الأدوار
                    //var roleResult = await _userManager.AddToRolesAsync(appUser, roles);

                    var roleResult = await _userManager.AddToRoleAsync(appUser, "Admin");

                    if (roleResult.Succeeded)
                    {
                        return Ok
                        (
                            new NewUserDto()
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = await _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, CreatedUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
