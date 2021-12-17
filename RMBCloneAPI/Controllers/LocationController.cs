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
        

        /// <response code="200">Vraća sve lokacije.</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<LocationDBModel>>> GetAll()
        {
            var result = await _locationData.GetAllLocationsAsync();
            return Ok(result);
        }

        /// <response code="200">Vraća lokaciju za zadanim id-om.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<LocationDBModel>> GetById(string id)
        {
            var result = await _locationData.FindAsync(id);
            return Ok(result);
        }

        /// <response code="200">Lokacija uspješno dodan.</response> 
        /// <response code="400">Body je neispravan.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddLocation(LocationRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var location = new LocationDBModel
                {
                    Address = model.Address,
                    Latitude = (decimal)model.Latitude,
                    Longitude = (decimal)model.Longitude
                };

                await _locationData.AddLocationAsync(location);
                return Ok();
            }
            return BadRequest();
        }


        /// <response code="204">Lokacija uspješno updateovana.</response> 
        /// <response code="400">Ili je body neispravan ili lokacija sa zadanim id-om ne postoji ili se ne podudaraju id iz querija i id iz bodija.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateLocation(string id, LocationDBModel model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }
                var location = await _locationData.FindAsync(id);
                if (location == null)
                {
                    return BadRequest();
                }
                location.Address = model.Address;
                location.Latitude = model.Latitude;
                location.Longitude = model.Longitude;

                await _locationData.UpdateLocationAsync(location);
                return NoContent();
            }
            return BadRequest();
        }




        /// <response code="204">Lokacija uspješno obrisana.</response> 
        /// <response code="400">Lokacija sa zadanim id-om ne postoji.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteLocation(string id)
        {
            var location = await _locationData.FindAsync(id);
            if (location == null)
            {
                return BadRequest();
            }
            await _locationData.DeleteLocationAsync(location.Id);
            return NoContent();
        }
    }
}
