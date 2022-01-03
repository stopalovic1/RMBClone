using Microsoft.AspNetCore.Mvc.Rendering;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace RMBCloneAPI.Helpers
{
    public interface IHelperMethods
    {
        List<SelectListItem> ATMFilter();
        List<SelectListItem> ATMType();
        string GenerateJwt(List<Claim> claims, DateTime notBefore, DateTime expires);
        List<SelectListItem> MapCities(List<CityDBModel> collection);
    }
}