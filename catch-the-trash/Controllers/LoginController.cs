using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using catch_the_trash.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace catch_the_trash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginController(SignInManager<User> SignInManager,
                               UserManager<User> UserManager
            )
        {
            _signInManager = SignInManager;
            _userManager = UserManager;

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginData) {
            var user = await _userManager.FindByNameAsync(loginData.UserName);
            if(user != null && 
                await _userManager.CheckPasswordAsync(user, loginData.Password))
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));
                return Ok();
            }
            return NotFound();
            
        }
    }
}