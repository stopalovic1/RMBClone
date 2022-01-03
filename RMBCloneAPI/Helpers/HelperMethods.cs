using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RMBCloneAPI.Helpers
{
    public class HelperMethods : IHelperMethods
    {
        public List<SelectListItem> ATMType()
        {
            var atmTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "Unutrašnji",
                    Text = "Unutrašnji"
                },
                new SelectListItem
                {
                    Value = "Vanjski",
                    Text = "Vanjski"
                }
            };
            return atmTypes;
        }

        public List<SelectListItem> ATMFilter()
        {
            var atmFilters = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "CashIn",
                    Text = "CashIn"
                },
                new SelectListItem
                {
                    Value = "CashOut",
                    Text = "CashOut"
                },
                new SelectListItem
                {
                    Value = "Bankomatiq",
                    Text = "Bankomatiq"
                },
                new SelectListItem
                {
                    Value = "Mjenjačnica",
                    Text = "Mjenjačnica"
                }
            };
            return atmFilters;
        }
        public List<SelectListItem> MapCities(List<CityDBModel> collection)
        {
            var list = collection.Select(x =>
              new SelectListItem
              {
                  Value = x.Id,
                  Text = x.Name

              }).ToList();
            return list;
        }


        public string GenerateJwt(List<Claim> claims, DateTime notBefore, DateTime expires)
        {
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mojsuperdupersikritkij")),
                    SecurityAlgorithms.HmacSha256)),
                    new JwtPayload("RmBClone", "Everyone", claims, notBefore, expires));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

    }
}
