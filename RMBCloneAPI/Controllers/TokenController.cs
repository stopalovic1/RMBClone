using Microsoft.AspNetCore.Http;
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
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using RmbRazorPages.Library.ApiClient;
using Microsoft.Extensions.Configuration;
using RmbClone.Library.Models.Requests;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserData _userData;
        private readonly IApiHelper _apiHelper;
        private readonly IConfiguration _config;

        public TokenController(IUserData userData, IApiHelper apiHelper, IConfiguration config)
        {
            _userData = userData;
            _apiHelper = apiHelper;
            _config = config;
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create(AuthRequestModel model)
        {
            if (await IsValidEmailAndPassword(model.Email, model.Password))
            {
                return new ObjectResult(await GenerateToken(model.Email));
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


            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var output = new
            {
                access_token = jwtToken,
                refresh_token = "ovdje ce bit refresh uskoro"
            };

            return output;
        }


        [HttpPost]
        [Route("/fcmnotification")]
        public async Task<IActionResult> SendFCMNotification(string deviceToken)
        {
            if (deviceToken == null)
            {
                return BadRequest();
            }

            var notification = new
            {
                to = deviceToken,
                data = new
                {
                    body = "kakoe rodjak",
                    title = "Testiranje"
                },
                priority = "high"
            };

            var serverKey = _config.GetSection("FcmNotifications").GetSection("ServerKey").Value;
            var senderId = _config.GetSection("FcmNotifications").GetSection("SenderId").Value;
            var json = JsonConvert.SerializeObject(notification);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://fcm.googleapis.com/fcm/send"),
                Method = HttpMethod.Post,
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.TryAddWithoutValidation("Authorization", $"key={serverKey}");
            request.Headers.TryAddWithoutValidation("Sender", $"id={senderId}");
            var meessage = await _apiHelper.ApiClient.SendAsync(request);
            if (meessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}
