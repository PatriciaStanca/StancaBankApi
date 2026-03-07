namespace StancaBankApi.Data.Repos;

public class DispositionRepo(BankAppDataContext db) : IDispositionRepo
{
    public List<Disposition> GetByCustomerId(int customerId) =>
        db.Dispositions.Where(d => d.CustomerId == customerId).ToList();

    public Disposition Add(Disposition disposition)
    {
        db.Dispositions.Add(disposition);
        db.SaveChanges();
        return disposition;
    }

    public void DeleteByCustomerId(int customerId)
    {
        var dispositions = db.Dispositions.Where(d => d.CustomerId == customerId).ToList();
        if (dispositions.Count == 0)
        {
            return;
        }

        db.Dispositions.RemoveRange(dispositions);
        db.SaveChanges();
    }

    public void DeleteByAccountId(int accountId)
    {
        var dispositions = db.Dispositions.Where(d => d.AccountId == accountId).ToList();
        if (dispositions.Count == 0)
        {
            return;
        }

        db.Dispositions.RemoveRange(dispositions);
        db.SaveChanges();
    }
}
