namespace StancaBankApi.Core.Services;

public class LoanService(ILoanRepo loanRepo) : ILoanService
{
    public List<LoanDTO> GetByCustomerId(int customerId) =>
        loanRepo.GetByCustomerId(customerId)
            .Select(loan =>
            {
                return new LoanDTO
                {
                    Id = loan.Id,
                    CustomerId = customerId,
                    DepositAccountId = loan.AccountId,
                    Amount = loan.Amount,
                    InterestRate = (decimal)loan.InterestRate,
                    CreatedAtUtc = loan.Date
                };
            })
            .ToList();
}
