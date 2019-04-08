using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TourAgency.WebApi.Data;
using TourAgency.WebApi.ViewModel;

namespace TourAgency.WebApi.Controllers
{
    [Authorize]
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager = null;
        private readonly UserManager<ApplicationUser> _userManager = null;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IdentityRole> GetRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            
            return role;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleForCreationViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("you didn't pass any information");
            }
           
            var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Role wasn't added to contex");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] RoleForDetailedViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("you didn't pass any information");
            }
            IdentityRole roleForUpdate = await _roleManager.FindByIdAsync(model.Id);
            roleForUpdate.Name = model.Name;
            roleForUpdate.NormalizedName = model.Name.ToUpper();
            var result = await _roleManager.UpdateAsync(roleForUpdate);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Role wasn't added to contex");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("you didn't pass any information");

            }
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest("Role wasn't deleted from contex");
        }
    }
}
