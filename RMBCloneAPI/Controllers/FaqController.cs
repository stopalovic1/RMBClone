using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RmbClone.Library.DataAccess;
using RmbClone.Library.Models;
using RmbCloneAPI.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
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

        /// <response code="200">Vraca listu faq.</response> 
        [HttpGet]
        public async Task<List<FaqDBModel>> GetAll()
        {
            var result = await _faqData.GetAllFaq();
            return result;
        }

        /// <response code="200">Faq kreiran.</response> 
        [HttpPost]
        public async Task AddFaq(FaqRequestModel model)
        {
            var faq = new FaqDBModel { Question = model.Question, Answer = model.Answer };
            await _faqData.AddFaq(faq);
        }

        /// <response code="204">Faq uspješno updateovan.</response> 
        /// <response code="400">Ili faq sa unesenim Idom ne postoji ili se id iz bodija i id iz querija ne podudraju.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateFaq(string id, FaqDBModel model)
        {
            if (ModelState.IsValid)
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
            return BadRequest();
        }


        /// <response code="204">Faq uspješno obrisan.</response> 
        /// <response code="400">Faq sa zadanim id-om ne postoji.</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
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
