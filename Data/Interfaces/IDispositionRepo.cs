namespace StancaBankApi.Data.Interfaces;

public interface IDispositionRepo
{
    List<Disposition> GetByCustomerId(int customerId);
    Disposition Add(Disposition disposition);
    void DeleteByCustomerId(int customerId);
    void DeleteByAccountId(int accountId);
}
