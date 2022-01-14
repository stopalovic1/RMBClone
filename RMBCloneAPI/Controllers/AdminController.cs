using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRoleData _roleData;
        private readonly IUserData _userData;

        public AdminController(IRoleData roleData, IUserData userData)
        {
            _roleData = roleData;
            _userData = userData;
        }


        [HttpPost]
        [Route("api/AddRole")]
        public async Task<IActionResult> AddRole(RoleRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new RoleDBModel { Name = model.Name };
                await _roleData.AddRoleAsync(role);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("api/AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(UserRoleRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userData.FindByIdAsync(model.UserId);
                    await _userData.AssignRoleAsync(user, model.RoleName);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(UserRoleRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userData.FindByIdAsync(model.UserId);
                    await _userData.RemoveRoleAsync(user, model.RoleName);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
