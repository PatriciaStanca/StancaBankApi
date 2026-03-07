namespace StancaBankApi.Core.Services;

public partial class AdminService
{
    public LoanDTO CreateLoan(CreateLoanDTO dto)
    {
        var customer = customerRepo.GetById(dto.CustomerId)
            ?? throw new ArgumentException("Kunden finns inte.");

        var account = accountRepo.GetById(dto.DepositAccountId)
            ?? throw new ArgumentException("Insättningskonto finns inte.");

        if (!accountRepo.IsOwnedByCustomer(account.Id, customer.Id))
        {
            throw new InvalidOperationException("Insättningskonto tillhör inte kunden.");
        }

        account.Balance += dto.Amount;
        accountRepo.Update(account);

        var loan = loanRepo.Add(new Loan
        {
            AccountId = account.Id,
            Amount = dto.Amount,
            InterestRate = (double)dto.InterestRate,
            Date = DateTime.UtcNow,
            Duration = 12,
            DurationMonths = 12,
            Payments = Math.Round(dto.Amount / 12m, 2),
            Status = "Active"
        });

        return new LoanDTO
        {
            Id = loan.Id,
            CustomerId = customer.Id,
            DepositAccountId = loan.AccountId,
            Amount = loan.Amount,
            InterestRate = (decimal)loan.InterestRate,
            CreatedAtUtc = loan.Date
        };
    }
}
