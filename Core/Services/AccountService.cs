namespace StancaBankApi.Core.Services;

public class AccountService(
    IAccountRepo accountRepo,
    IAccountTypeRepo accountTypeRepo,
    ITransactionRepo transactionRepo,
    IDispositionRepo dispositionRepo) : IAccountService
{
    private AccountDTO ToAccountDto(Account account) => new()
    {
        Id = account.Id,
        AccountNumber = account.Id.ToString(),
        AccountType = accountTypeRepo.GetById(account.AccountTypeId ?? 0)?.Name ?? "Okänd",
        Balance = account.Balance
    };

    public List<AccountDTO> GetCustomerAccounts(int customerId)
    {
        return accountRepo.GetByCustomerId(customerId)
            .Select(ToAccountDto)
            .ToList();
    }

    public AccountDTO CreateAccountForCustomer(int customerId, int accountTypeId)
    {
        if (accountTypeRepo.GetById(accountTypeId) is null)
        {
            throw new ArgumentException("Kontotyp finns inte.");
        }

        var account = accountRepo.Add(new Account
        {
            AccountTypeId = accountTypeId,
            Balance = 0m,
            Created = DateTime.UtcNow,
            Frequency = "Monthly"
        });

        dispositionRepo.Add(new Disposition
        {
            CustomerId = customerId,
            AccountId = account.Id,
            Role = "OWNER"
        });

        return ToAccountDto(account);
    }

    public AccountDTO UpdateAccountType(int customerId, int accountId, int accountTypeId)
    {
        var account = accountRepo.GetById(accountId)
            ?? throw new ArgumentException("Kontot finns inte.");

        if (!accountRepo.IsOwnedByCustomer(accountId, customerId))
        {
            throw new UnauthorizedAccessException("Du kan bara uppdatera dina egna konton.");
        }

        if (accountTypeRepo.GetById(accountTypeId) is null)
        {
            throw new ArgumentException("Kontotyp finns inte.");
        }

        account.AccountTypeId = accountTypeId;
        accountRepo.Update(account);

        return ToAccountDto(account);
    }

    public void DeleteAccount(int customerId, int accountId)
    {
        var account = accountRepo.GetById(accountId)
            ?? throw new ArgumentException("Kontot finns inte.");

        if (!accountRepo.IsOwnedByCustomer(accountId, customerId))
        {
            throw new UnauthorizedAccessException("Du kan bara ta bort dina egna konton.");
        }

        if (account.Balance != 0m)
        {
            throw new InvalidOperationException("Kontot måste ha saldo 0 innan borttagning.");
        }

        if (transactionRepo.HasForAccount(accountId))
        {
            throw new InvalidOperationException("Kontot kan inte tas bort eftersom det har transaktionshistorik.");
        }

        dispositionRepo.DeleteByAccountId(accountId);
        accountRepo.Delete(accountId);
    }
}
