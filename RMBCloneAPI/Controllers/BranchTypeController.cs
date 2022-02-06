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
    public class BranchTypeController : ControllerBase
    {
        private readonly IBranchTypeData _branchTypeData;

        public BranchTypeController(IBranchTypeData branchTypeData)
        {
            _branchTypeData = branchTypeData;
        }
        [HttpGet]
        public async Task<ActionResult<List<BranchTypeDBModel>>>GetAll()
        {
            try
            {
                var result = await _branchTypeData.GetBranchTypesAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult>Insert(BranchTypeRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var branchType = new BranchTypeDBModel
                {
                    Name = model.Name
                };
                await _branchTypeData.InsertBranchTypeAsync(branchType);
                return Ok();
            }
            return BadRequest();
           
        }
    }
}
