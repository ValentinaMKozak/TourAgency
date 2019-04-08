using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TourAgency.WebApi.Data;
using TourAgency.WebApi.ViewModel;

namespace TourAgency.WebApi.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> GetUsers()
        {
            var users = _userManager.Users; 
            return users;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            return BadRequest("User wasn't saved");
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetAllUserRoles(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Ok(model);
            }
            return NotFound();
        }
      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserViewModel model) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(id);
            
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return Ok();
            }
            return BadRequest("User wasn't updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return Ok();
            }
            return BadRequest("User doesn't exist");
        }
    }
}