namespace StancaBankApi.Data.Repos;

public class AccountRepo(BankAppDataContext db) : IAccountRepo
{
    public Account? GetById(int id) => db.Accounts.FirstOrDefault(a => a.Id == id);

    public Account? GetByAccountNumber(string accountNumber) =>
        int.TryParse(accountNumber, out var accountId) ? GetById(accountId) : null;

    public List<Account> GetByCustomerId(int customerId) =>
        db.Accounts
            .Where(a => db.Dispositions.Any(d => d.AccountId == a.Id && d.CustomerId == customerId))
            .ToList();

    public bool IsOwnedByCustomer(int accountId, int customerId) =>
        db.Dispositions.Any(d => d.AccountId == accountId && d.CustomerId == customerId);

    public Account Add(Account account)
    {
        db.Accounts.Add(account);
        db.SaveChanges();
        return account;
    }

    public void Update(Account account)
    {
        var existing = GetById(account.Id) ?? throw new InvalidOperationException("Account not found");
        existing.Balance = account.Balance;
        existing.AccountTypeId = account.AccountTypeId;
        existing.Frequency = account.Frequency;
        existing.Created = account.Created;
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        var account = GetById(id) ?? throw new InvalidOperationException("Account not found");
        db.Accounts.Remove(account);
        db.SaveChanges();
    }

    public void DeleteByCustomerId(int customerId)
    {
        var accountIds = db.Dispositions
            .Where(d => d.CustomerId == customerId)
            .Select(d => d.AccountId)
            .Distinct()
            .ToList();

        var accounts = db.Accounts.Where(a => accountIds.Contains(a.Id)).ToList();
        if (accounts.Count == 0)
        {
            return;
        }

        db.Accounts.RemoveRange(accounts);
        db.SaveChanges();
    }
}
