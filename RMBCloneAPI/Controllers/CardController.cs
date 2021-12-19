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
    public class CardController : ControllerBase
    {
        private readonly ICardData _cardData;
        private readonly IUserData _userData;

        public CardController(ICardData cardData,IUserData userData)
        {
            _cardData = cardData;
            _userData = userData;
        }
        [HttpGet]
        public async Task<ActionResult<List<CardDBModel>>> GetAll()
        {
            var result = await _cardData.GetAllCardsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardDBModel>> GetById(string id)
        {
            var result = await _cardData.FindAsync(id);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCard(CardRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //var user= await _userData.f
                var card = new CardDBModel
                {
                    UserId = model.UserId,
                    Iban = "BA39" + model.TransactionNumber,
                    CardNumber = model.CardNumber,
                    ValidUntil = model.ValidUntil,
                    TransactionNumber = model.TransactionNumber
                };
                await _cardData.AddCardAsync(card);
                return Ok();
            }
            return BadRequest("Body neispravan");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(string id, CardDBModel model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id)
                {
                    return BadRequest("Id-evi se ne podudaraju.");
                }
                var card = await _cardData.FindAsync(id);
                if (card == null)
                {
                    return BadRequest("Kartica sa tim id-om ne postoji.");
                }
                card.UserId = model.UserId;
                card.Iban = model.Iban;
                card.CardNumber = model.CardNumber;
                card.ValidUntil = model.ValidUntil;
                card.TransactionNumber = model.TransactionNumber;
                await _cardData.UpdateCardAsync(card);
                return NoContent();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(string id)
        {
            var card = await _cardData.FindAsync(id);
            if (card == null)
            {
                return BadRequest("Kartica sa tim id-om ne postoji.");
            }
            await _cardData.DeleteCardAsync(card.Id);
            return NoContent();
        }
    }
}
