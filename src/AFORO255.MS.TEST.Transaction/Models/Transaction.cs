using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Transactions.Models
{
    public class Transaction
    {
        [BsonId]
        [BsonElement("id_transaction")]
        public int Id { get; set; }

        [BsonElement("id_invoice")]
        public int InvoiceId { get; set; }

        [BsonElement("amount")]
        public decimal Amount { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}
