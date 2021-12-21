using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models.Requests;
using RmbClone.Library.Models.Responses;
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

        /// <response code="200">Vra</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<BranchResponseModel>>>GetAll()
        {
            var result = await _branchData.GetAllBranchesAsync();
            return Ok(result);
        }

        /// <response code="200">Poslovnica uspješno dodana.</response> 
        /// <response code="400">Body je neispravan.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddBranch(BranchRequestModel model)
        {
            if(ModelState.IsValid)
            {
                await _branchData.InsertBranchAsync(model);
                return Ok();
            }
            return BadRequest();
            
        }
    }
}
