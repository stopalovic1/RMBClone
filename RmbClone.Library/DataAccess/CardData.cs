using RmbClone.Library.Internal.DataAccess;
using RmbClone.Library.Models;
using RmbClone.Library.Models.HelperModels;
using RmbClone.Library.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmbClone.Library.DataAccess
{
    public class CardData : ICardData
    {
        private readonly ISqlDataAccess _sql;
        private readonly ITransactionData _transactionData;

        public CardData(ISqlDataAccess sql, ITransactionData transactionData)
        {
            _sql = sql;
            _transactionData = transactionData;
        }

        public async Task<List<CardResponseModel>> GetAllCardsForUserAsync(string userId)
        {

            var cards = await _sql.LoadDataAsync<CardDBModel, dynamic>("dbo.spCard_LookupByUserId", new { UserId = userId }, "RmbCloneDb");
            var response = new List<CardResponseModel>();
            var tasks = cards.Select(async card =>
            {
                var sentTransactionsFromDb = await _transactionData.GetAllSentTransactionsAsync(card.TransactionNumber);
                var sentTransactions = sentTransactionsFromDb.Select(x =>
                {
                    return new SentTransactionsModel
                    {
                        Id = x.Id,
                        SentAmount = x.TransactionValue,
                        DateOfSending = x.DateOfTransaction,
                        ReceiverTransactionNumber = x.ToAccount,
                        TransactionReason = x.TransactionReason,
                        SentTo = "a"
                    };
                }).ToList();

                var receivedTransactionsFromDb = await _transactionData.GetAllReceivedTransactionsAsync(card.TransactionNumber);
                var receivedTransactions = receivedTransactionsFromDb.Select(x =>
                {
                    return new ReceivedTransactionsModel
                    {
                        Id = x.Id,
                        ReceivedAmount = x.TransactionValue,
                        DateOfReceiving = x.DateOfTransaction,
                        TransactionReason = x.TransactionReason,
                        SenderTransactionNumber = x.FromAccount,
                        ReceivedFrom = "a"
                    };
                }).ToList();

                var map = new CardResponseModel
                {
                    Id = card.Id,
                    CurrentAmount = card.CurrentAmount,
                    CardNumber = card.CardNumber,
                    HasLimit = card.HasLimit,
                    Iban = card.Iban,
                    LimitAmount = card.LimitAmount,
                    SentTransactions = sentTransactions,
                    ReceivedTransactions = receivedTransactions,
                    TransactionNumber = card.TransactionNumber,
                    ValidUntil = card.ValidUntil
                };
                response.Add(map);
            });

            await Task.WhenAll(tasks);
            return response;
        }

        public async Task AddCardAsync(CardDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spCard_Insert", model, "RmbCloneDb");
        }

        public async Task DeleteCardAsync(string id)
        {
            await _sql.SaveDataAsync("dbo.spCard_Delete", new { Id = id }, "RmbCloneDb");
        }

        public async Task<CardDBModel> FindAsync(string id)
        {
            var result = await _sql.LoadDataAsync<CardDBModel, dynamic>("dbo.spCard_LookupById", new { Id = id }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task<CardDBModel> FindByTransactionNumberAsync(string transactionNumber)
        {
            var result = await _sql.LoadDataAsync<CardDBModel, dynamic>("dbo.spCard_LookupByTransactionNumber", new { TransactionNumber = transactionNumber }, "RmbCloneDb");
            return result.FirstOrDefault();
        }

        public async Task UpdateCardAsync(CardDBModel model)
        {
            await _sql.SaveDataAsync("dbo.spCard_Update", model, "RmbCloneDb");
        }
    }
}
