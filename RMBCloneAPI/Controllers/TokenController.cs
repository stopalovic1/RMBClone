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
using RmbClone.Library.Models.Responses;
using RmbClone.Library.Models;
using RMBCloneAPI.Helpers;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserData _userData;
        private readonly IApiHelper _apiHelper;
        private readonly IConfiguration _config;
        private readonly IHelperMethods _helperMethods;

        public TokenController(IUserData userData, IApiHelper apiHelper, IConfiguration config, IHelperMethods helperMethods)
        {
            _userData = userData;
            _apiHelper = apiHelper;
            _config = config;
            _helperMethods = helperMethods;
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




        private async Task<AuthResponseModel> GenerateToken(string email)
        {
            var user = await _userData.FindByEmailAsync(email);

            var accessClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                //new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddMinutes(1)).ToUnixTimeSeconds().ToString())
            };

            var accessToken = _helperMethods.GenerateJwt(accessClaims, DateTime.Now, DateTime.Now.AddMinutes(1));

            var refreshClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                //new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                //new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var refreshToken = _helperMethods.GenerateJwt(refreshClaims, DateTime.Now, DateTime.Now.AddDays(1));


            return new AuthResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        }

        private ClaimsPrincipal GetClaimsPrincipalFromToken(string token)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mojsuperdupersikritkij")),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            };

            var principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return principal;
        }

        [HttpPost]
        [Route("/api/refresh-token")]
        public async Task<ActionResult<AuthResponseModel>> RefreshToken(AuthResponseModel model)
        {
            var task = Task.Run(() => RefreshAccessToken(model.AccessToken, model.RefreshToken));
            return await task;
        }


        private ActionResult<AuthResponseModel> RefreshAccessToken(string token, string refreshToken)
        {
            var validateToken = GetClaimsPrincipalFromToken(token);
            var validateRefreshToken = GetClaimsPrincipalFromToken(refreshToken);

            if (validateToken == null || validateRefreshToken == null)
            {
                return BadRequest("Invalid token");
            }

            long.TryParse(validateToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value, out var expOfAccessToken);
            long.TryParse(validateRefreshToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value, out var expOfRefreshToken);

            var expiryDateOfAccessToken = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expOfAccessToken).ToLocalTime();
            var expiryDateOfRefreshToken = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expOfRefreshToken).ToLocalTime();

            if (expiryDateOfAccessToken > DateTime.Now)
            {
                return BadRequest("Access token nije jos isteko rodjak");
            }

            if (expiryDateOfRefreshToken < DateTime.Now)
            {
                return BadRequest("Refresh isteko");
            }

            var idFromAccessToken = validateToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var idFromRefreshToken = validateRefreshToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (idFromAccessToken != idFromRefreshToken)
            {
                return BadRequest("Id iz refresh tokena ne odgovara id-u iz access tokena.");
            }

            var email = validateToken.Identity.Name;
            var response = new AuthResponseModel
            {
                AccessToken = _helperMethods.GenerateJwt(validateToken.Claims.ToList(), DateTime.Now, DateTime.Now.AddMinutes(1)),
                RefreshToken = _helperMethods.GenerateJwt(validateRefreshToken.Claims.ToList(), DateTime.Now, DateTime.Now.AddDays(1))
            };

            return response;
        }


        [HttpPost]
        [Route("/api/fcmnotification")]
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
