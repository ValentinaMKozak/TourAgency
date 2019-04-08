using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TourAgency.WebApi.Data;
using TourAgency.WebApi.Helpers;
using TourAgency.WebApi.ViewModel;

namespace TourAgency.WebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if(userExists != null)
                ModelState.AddModelError("Email", "Email already exists. Сhoose another");

            if (!ModelState.IsValid)
            {
                return BadRequest("model for register isn't valid");
            }

            ApplicationUser user = new ApplicationUser { Email = model.Email, UserName = model.UserName };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                IdentityRole role = await _roleManager.FindByNameAsync("user");
                List<string> roles = new List<string>
                {
                    role.Name
                };
                if (role != null)
                {
                    await _userManager.AddToRolesAsync(user, roles);
                }
                return Ok();
            }
            return BadRequest("Account wasn't saved");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model for login isn't valid");
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest();

            var userRoles = await _userManager.GetRolesAsync(user);

            ClaimsIdentity identity = GetClaimsIdentity(user, userRoles);

            var tokenString = GetJwtToken(identity);
            return Ok(new { tokenString });
        }
        
        private string GetJwtToken(ClaimsIdentity identity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("super secret key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString ;
        }

        private ClaimsIdentity GetClaimsIdentity(ApplicationUser user, IList<string> roles)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            return claimsIdentity;
        }
    }
}








//[HttpGet]
//public async Task<ActionResult<string>> Get()
//{
//    //           "userName": "mike",
//    //"email": "mike@gmail.com",
//    //"password": "Password123!"

//    //var result = await _userManager.CreateAsync(
//    //new ApplicationUser()
//    //{
//    //    UserName = "valya",
//    //    Email = "valya@gmail.com"
//    //}, "Password123!");

//    //if (result.Succeeded) {
//    //    return "ok";
//    //}

//    var user = await _userManager.FindByNameAsync("valya");
//    var res2 = await _signInManager.CheckPasswordSignInAsync(user, "Password123!", false);
//    if (res2.Succeeded)
//    {
//        return "ok";
//    }
//    return "no";

//}