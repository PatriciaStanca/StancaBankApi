namespace StancaBankApi.Data.Interfaces;

public interface ILoanRepo
{
    List<Loan> GetByCustomerId(int customerId);
    Loan Add(Loan loan);
    void DeleteByCustomerId(int customerId);
}
