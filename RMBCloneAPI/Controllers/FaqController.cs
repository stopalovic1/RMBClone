using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IFaqData _faqData;

        public FaqController(IFaqData faqData)
        {
            _faqData = faqData;
        }

        [HttpGet]
        public async Task<List<FaqDBModel>> GetAll()
        {
            var result = await _faqData.getAllFaq();
            return result;
        }

        [HttpPost]
        public async Task AddFaq(FaqRequestModel model)
        {
            await _faqData.AddFaq(model);
        }

    }
}
