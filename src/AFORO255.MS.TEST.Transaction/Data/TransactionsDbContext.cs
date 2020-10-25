using MongoDB.Driver;
using Transactions.Models;

namespace Transactions.Data
{
    public class TransactionsDbContext : ITransactionsDbContext
    {
        public IMongoCollection<Transaction> Transactions { get; }

        public TransactionsDbContext(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.Database);

            Transactions = database.GetCollection<Transaction>(nameof(Transaction));
        }
    }
}