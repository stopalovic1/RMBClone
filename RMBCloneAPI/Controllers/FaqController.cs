using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RmbCloneAPI.Models;
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
            var faq = new FaqDBModel { Question = model.Question, Answer = model.Answer };
            await _faqData.AddFaq(faq);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFaq(string id, FaqDBModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var faq = await _faqData.FindAsync(id);
            if (faq == null)
            {
                return BadRequest();
            }

            faq.Question = model.Question;
            faq.Answer = model.Answer;

            await _faqData.UpdateFaq(faq);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaq(string id)
        {
            var faq = await _faqData.FindAsync(id);
            if (faq == null)
            {
                return BadRequest();
            }
            await _faqData.DeleteFaq(faq.Id);
            return NoContent();
        }
    }
}
