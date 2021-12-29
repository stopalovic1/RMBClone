﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using RmbClone.Library.DataAccess;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserData _userData;

        public TokenController(IUserData userData)
        {
            _userData = userData;
        }


        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string email, string password)
        {
            if (await IsValidEmailAndPassword(email, password))
            {
                return new ObjectResult(await GenerateToken(email));
            }
            else
            {
                return BadRequest("Wrong email or password.");
            }
        }

        private async Task<bool> IsValidEmailAndPassword(string email, string password)
        {
            var user = await _userData.FindByEmailAsync(email);
            var result = await _userData.CheckPasswordAsync(user, password);
            return result;
        }

        private async Task<dynamic> GenerateToken(string email)
        {
            var user = await _userData.FindByEmailAsync(email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mojsuperdupersikritkij")),
                    SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims));


            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            return access_token;
        }

    }
}
