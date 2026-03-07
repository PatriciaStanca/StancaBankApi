namespace StancaBankApi.Core.Interfaces;

public interface ILoanService
{
    List<LoanDTO> GetByCustomerId(int customerId);
}
