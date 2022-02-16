using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models.Requests;
using RmbClone.Library.Models.Responses;
using RMBCloneAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchData _branchData;

        public BranchController(IBranchData branchData)
        {
            _branchData = branchData;
        }

        /// <response code="200">Vraca sve brancheve</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<BranchResponseModel>>> GetAll()
        {
            try
            {
                var result = await _branchData.GetAllBranchesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <response code="200">Poslovnica uspješno dodana.</response> 
        /// <response code="400">Body je neispravan.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBranch(BranchRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _branchData.InsertBranchAsync(model);
                return Ok();
            }
            return BadRequest();

        }


        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteBranch(string id)
        {
            var branch = await _branchData.GetBranchByIdAsync(id);
            if (branch == null)
            {
                return BadRequest("Branch sa ovim id-om ne postoji.");
            }
            try
            {
                await _branchData.DeleteBranchAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateBranch(string id, BranchRequestModel model)
        {
            var branch = await _branchData.GetBranchByIdAsync(id);
            if (branch == null)
            {
                return BadRequest("Branch sa ovim id-om ne postoji.");
            }
            try
            {
                await _branchData.UpdateBranchAsync(id, model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("FilterBranches")]
        public async Task<ActionResult<List<BranchResponseModel>>> FilterBranches([FromQuery] string city = null, [FromQuery] string branchType = null, 
            [FromQuery] string branchServiceType = null, [FromQuery] string atmType = null, double? radius = null,double? latitude=null,double? longitude=null)
        {
            try
            {
                var result = await _branchData.GetFilteredBranchesAsync(city, branchType, branchServiceType, atmType, radius, latitude,longitude);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
