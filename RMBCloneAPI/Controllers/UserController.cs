using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.Models;
using RMBCloneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using RmbClone.Library.DataAccess;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData _userData;

        public UserController(IUserData userData)
        {
            _userData = userData;
        }
        /// <response code="200">Korisnik kreiran.</response> 
        /// <response code="400">Ili korsnik sa unesenim emailom već postoji ili se passwordi ne podudraju.</response> 
        [HttpPost]
        public async Task<IActionResult> AddUser(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userData.FindByEmailAsync(model.Email);
                if(user!=null)
                {
                    return BadRequest("User with that email already exists.");
                }

                var data = new UserDBModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password, hashType: HashType.SHA256),
                    FcmToken = null
                };
                await _userData.AddUserAsync(data);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<List<UserDBModel>> GetUsers()
        {
            var result = await _userData.GetAllUsersAsync();
            return result;
        }
    }
}
