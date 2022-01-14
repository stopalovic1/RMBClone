using Microsoft.AspNetCore.Authorization;
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
    [SkipStatusCodePages]
    public class CityController : ControllerBase
    {
        private readonly ICityData _cityData;

        public CityController(ICityData cityData)
        {
            _cityData = cityData;
        }

        /// <response code="200">Vraća sve gradove.</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<List<CityDBModel>>> GetAll()
        {
            var result = await _cityData.GetAllCitiesAsync();
            return Ok(result);
        }

        /// <response code="200">Vraća grad sa zadnim id-om.</response> 
        /// <response code="400">Grad sa zadanim id-om ne postoji.</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CityDBModel>> GetById(string id)
        {
            var result = await _cityData.FindAsync(id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        /// <response code="200">Grad uspješno dodan.</response> 
        /// <response code="400">Body je neispravan.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddCity(CityRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var city = new CityDBModel { Name = model.Name };
                await _cityData.AddCityAsync(city);
                return Ok();
            }
            return BadRequest();
        }

        /// <response code="204">Grad uspješno updateovan.</response> 
        /// <response code="400">Ili je body neispravan ili grad sa zadanim idom ne postoji ili se ne podudaraju id iz querija i id iz bodija.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateCity(string id, CityDBModel model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest();
                }
                var city = await _cityData.FindAsync(id);
                if (city == null)
                {
                    return BadRequest();
                }
                city.Name = model.Name;
                await _cityData.UpdateCityAsync(city);
                return NoContent();
            }
            return BadRequest();
        }


        /// <response code="204">Grad uspješno obrisan.</response> 
        /// <response code="400">Grad sa zadanim id-om ne postoji.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteCity(string id)
        {
            var city = await _cityData.FindAsync(id);
            if (city == null)
            {
                return BadRequest();
            }
            await _cityData.DeleteCityAsync(city.Id);
            return NoContent();
        }
    }
}
