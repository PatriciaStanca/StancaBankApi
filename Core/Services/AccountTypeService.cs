namespace StancaBankApi.Core.Services;

public class AccountTypeService(IAccountTypeRepo accountTypeRepo) : IAccountTypeService
{
    public List<AccountType> GetAll() => accountTypeRepo.GetAll();
}
