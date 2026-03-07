namespace StancaBankApi.Data.Repos;

public class TransactionRepo(BankAppDataContext db) : ITransactionRepo
{
    public List<Transaction> GetByAccountId(int accountId) =>
        db.Transactions.Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.Date)
            .ToList();

    public Transaction Add(Transaction transaction)
    {
        db.Transactions.Add(transaction);
        db.SaveChanges();
        return transaction;
    }

    public bool HasForAccount(int accountId) =>
        db.Transactions.Any(t => t.AccountId == accountId);

    public void DeleteByAccountIds(List<int> accountIds)
    {
        if (accountIds.Count == 0)
        {
            return;
        }

        var transactions = db.Transactions
            .Where(t => accountIds.Contains(t.AccountId))
            .ToList();

        if (transactions.Count == 0)
        {
            return;
        }

        db.Transactions.RemoveRange(transactions);
        db.SaveChanges();
    }
}
