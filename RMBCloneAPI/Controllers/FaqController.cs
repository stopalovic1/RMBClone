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
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<FaqDBModel>>> GetAll()
        {
            var result = await _faqData.GetAllFaq();
            return Ok(result);
        }

        /// <response code="200">Vraca faq sa zadanim id-om.</response> 
        /// <response code="400">Faq sa zadanim id-om ne postoji.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<FaqDBModel>> GetById(string id)
        {
            var result = await _faqData.FindAsync(id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        /// <response code="200">Faq kreiran.</response> 
        /// <response code="400">Body nije ispravan.</response> 
        [HttpPost]
        public async Task<IActionResult> AddFaq(FaqRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var faq = new FaqDBModel
                {
                    QuestionBj = model.QuestionBj,
                    AnswerBj = model.AnswerBj,
                    QuestionEn = model.QuestionEn,
                    AnswerEn = model.AnswerEn
                };
                await _faqData.AddFaq(faq);
                return Ok();
            }
            return BadRequest();

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

                faq.QuestionBj = model.QuestionBj;
                faq.AnswerBj = model.AnswerBj;

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
