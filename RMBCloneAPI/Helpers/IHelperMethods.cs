using Microsoft.AspNetCore.Mvc.Rendering;
using RmbClone.Library.Models;
using System.Collections.Generic;

namespace RMBCloneAPI.Helpers
{
    public interface IHelperMethods
    {
        List<SelectListItem> ATMFilter();
        List<SelectListItem> ATMType();
        List<SelectListItem> MapCities(List<CityDBModel> collection);
    }
}