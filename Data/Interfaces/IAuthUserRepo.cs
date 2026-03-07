namespace StancaBankApi.Data.Interfaces;

public interface IAuthUserRepo
{
    AuthUser? GetByUsername(string username);
    AuthUser? GetByCustomerId(int customerId);
    AuthUser Add(AuthUser authUser);
    void Update(AuthUser authUser);
    void DeleteByCustomerId(int customerId);
}
