using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RMBCloneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationData _locationData;

        public LocationController(ILocationData locationData)
        {
            _locationData = locationData;
        }


        [HttpGet]
        public async Task<List<LocationDBModel>> GetAll()
        {
            var result = await _locationData.GetAllLocationsAsync();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var location = new LocationDBModel
                {
                    Address = model.Address,
                    Latitude = (double)model.Latitude,
                    Longitude = (double)model.Longitude
                };

                await _locationData.AddLocationAsync(location);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(string id)
        {
            var location = await _locationData.FindAsync(id);
            if (location == null)
            {
                return BadRequest();
            }
            await _locationData.DeleteLocationAsync(location.Id);
            return Ok();
        }
    }
}
