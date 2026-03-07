namespace StancaBankApi.Core.Interfaces;

public interface ITransactionService
{
    List<TransactionDTO> GetByAccountId(int customerId, int accountId);
    TransactionDTO Transfer(int customerId, TransferDTO dto);
}
