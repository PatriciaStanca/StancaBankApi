namespace StancaBankApi.Data.Interfaces;

public interface IAccountTypeRepo
{
    List<AccountType> GetAll();
    AccountType? GetById(int id);
}
