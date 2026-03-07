namespace StancaBankApi.Data.Repos;

public class AccountTypeRepo(BankAppDataContext db) : IAccountTypeRepo
{
    public List<AccountType> GetAll() => db.AccountTypes.ToList();
    public AccountType? GetById(int id) => db.AccountTypes.FirstOrDefault(a => a.Id == id);
}
