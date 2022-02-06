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
    public class BranchServiceTypeController : ControllerBase
    {
        private readonly IBranchServiceTypeData _branchServiceTypeData;

        public BranchServiceTypeController(IBranchServiceTypeData branchServiceTypeData)
        {
            _branchServiceTypeData = branchServiceTypeData;
        }
        [HttpGet]
        public async Task<ActionResult<List<BranchServiceTypeDBModel>>> GetAll()
        {
            var result = await _branchServiceTypeData.GetAllBranchServiceTypesAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> InsertBranchServiceType(BranchServiceTypeRequestModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var branchServiceType = new BranchServiceTypeDBModel
                    {
                        Name = model.Name,
                        NameBj = model.NameBj,
                        NameDe = model.NameDe,
                        NameEn = model.NameEn
                    };
                    await _branchServiceTypeData.InsertBranchServiceTypeAsync(branchServiceType);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
