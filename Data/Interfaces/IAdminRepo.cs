namespace StancaBankApi.Data.Interfaces;

public interface IAdminRepo
{
    AuthUser? GetByUsername(string username);
}
