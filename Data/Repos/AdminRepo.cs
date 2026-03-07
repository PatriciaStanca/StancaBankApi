namespace StancaBankApi.Data.Repos;

public class AdminRepo(BankAppDataContext db) : IAdminRepo
{
    public AuthUser? GetByUsername(string username) =>
        db.AuthUsers.FirstOrDefault(a =>
            a.Role.ToLower() == "admin" &&
            a.Username.ToLower() == username.Trim().ToLower());
}
