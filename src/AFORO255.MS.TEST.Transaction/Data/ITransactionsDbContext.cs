using MongoDB.Driver;
using Transactions.Models;

namespace Transactions.Data
{
    public interface ITransactionsDbContext
    {
        IMongoCollection<Transaction> Transactions { get; }
    }
}