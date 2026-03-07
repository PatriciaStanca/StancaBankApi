namespace StancaBankApi.Data.Repos;

public class LoanRepo(BankAppDataContext db) : ILoanRepo
{
    public List<Loan> GetByCustomerId(int customerId)
    {
        var accountIds = db.Dispositions
            .Where(d => d.CustomerId == customerId)
            .Select(d => d.AccountId)
            .Distinct()
            .ToList();

        return db.Loans.Where(l => accountIds.Contains(l.AccountId)).ToList();
    }

    public Loan Add(Loan loan)
    {
        db.Loans.Add(loan);
        db.SaveChanges();
        return loan;
    }

    public void DeleteByCustomerId(int customerId)
    {
        var accountIds = db.Dispositions
            .Where(d => d.CustomerId == customerId)
            .Select(d => d.AccountId)
            .Distinct()
            .ToList();

        var loans = db.Loans.Where(l => accountIds.Contains(l.AccountId)).ToList();
        if (loans.Count == 0)
        {
            return;
        }

        db.Loans.RemoveRange(loans);
        db.SaveChanges();
    }
}
