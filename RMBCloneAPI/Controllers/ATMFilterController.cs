using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RmbClone.Library.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMFilterController : ControllerBase
    {
        private readonly IATMFilterData _atmFilterData;

        public ATMFilterController(IATMFilterData atmFilterData)
        {
            _atmFilterData = atmFilterData;
        }


        [HttpGet]
        public async Task<ActionResult<List<ATMFilterDBModel>>> GetAll()
        {
            var result = await _atmFilterData.GetAllAtmFiltersAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertAtmFilter(ATMFilterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var atm = new ATMFilterDBModel
                {
                    Name = model.Name
                };

                await _atmFilterData.InsertAtmFilterAsync(atm);
                return Ok();
            }
            return BadRequest();
        }


    }
}
