namespace StancaBankApi.Core.Services;

public class TransactionService(
    ITransactionRepo transactionRepo,
    IAccountRepo accountRepo) : ITransactionService
{
    private static TransactionDTO ToDto(Transaction transaction) => new()
    {
        Id = transaction.Id,
        AccountId = transaction.AccountId,
        Date = transaction.Date,
        Type = transaction.Type,
        Operation = transaction.Operation,
        Amount = transaction.Amount,
        Balance = transaction.Balance,
        Symbol = transaction.Symbol,
        Bank = transaction.Bank,
        Account = transaction.CounterpartyAccount
    };

    public List<TransactionDTO> GetByAccountId(int customerId, int accountId)
    {
        var account = accountRepo.GetById(accountId);
        if (account is null || !accountRepo.IsOwnedByCustomer(accountId, customerId))
        {
            throw new UnauthorizedAccessException("Konto saknas eller tillhör annan kund.");
        }

        return transactionRepo.GetByAccountId(accountId).Select(ToDto).ToList();
    }

    public TransactionDTO Transfer(int customerId, TransferDTO dto)
    {
        var from = accountRepo.GetById(dto.FromAccountId)
            ?? throw new ArgumentException("Från-konto hittades inte.");

        if (!accountRepo.IsOwnedByCustomer(from.Id, customerId))
        {
            throw new UnauthorizedAccessException("Du kan bara överföra från dina egna konton.");
        }

        var to = accountRepo.GetByAccountNumber(dto.ToAccountNumber)
            ?? throw new ArgumentException("Mottagarkonto hittades inte.");

        if (from.Balance < dto.Amount)
        {
            throw new InvalidOperationException("Otillräckligt saldo.");
        }

        from.Balance -= dto.Amount;
        to.Balance += dto.Amount;
        accountRepo.Update(from);
        accountRepo.Update(to);

        var outTx = transactionRepo.Add(new Transaction
        {
            AccountId = from.Id,
            Date = DateTime.UtcNow,
            Type = "Transfer",
            Operation = "Debit",
            Amount = dto.Amount,
            Balance = from.Balance,
            Symbol = "TRF",
            Bank = "StancaBank",
            CounterpartyAccount = to.Id.ToString()
        });

        transactionRepo.Add(new Transaction
        {
            AccountId = to.Id,
            Date = DateTime.UtcNow,
            Type = "Transfer",
            Operation = "Credit",
            Amount = dto.Amount,
            Balance = to.Balance,
            Symbol = "TRF",
            Bank = "StancaBank",
            CounterpartyAccount = from.Id.ToString()
        });

        return ToDto(outTx);
    }
}
