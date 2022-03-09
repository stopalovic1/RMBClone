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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionData _transactionData;
        private readonly ICardData _cardData;

        public TransactionsController(ITransactionData transactionData, ICardData cardData)
        {
            _transactionData = transactionData;
            _cardData = cardData;
        }



        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionRequestModel model)
        {
            try
            {


                var senderCard = await _cardData.FindByTransactionNumberAsync(model.SenderTransactionNumber);
                //var receiverCard = await _cardData.FindByTransactionNumberAsync(model.ReceiverTransactionNumber);

                if (senderCard == null) //||receiverCard == null)
                {
                    return BadRequest("Korisnik ne postoji!");
                }
                var transaction = new TransactionDBModel
                {
                    CardId = senderCard.Id,
                    DateOfTransaction = DateTime.Now,
                    FromAccount = senderCard.TransactionNumber,
                    ToAccount = model.ReceiverTransactionNumber,
                    TransactionReason = model.TransactionReason,
                    TransactionValue = model.TransactionValue
                };

                await _transactionData.InsertTransactionAsync(transaction);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
