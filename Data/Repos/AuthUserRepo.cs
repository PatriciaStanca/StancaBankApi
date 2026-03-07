namespace StancaBankApi.Data.Repos;

public class AuthUserRepo(BankAppDataContext db) : IAuthUserRepo
{
    public AuthUser? GetByUsername(string username) =>
        db.AuthUsers.FirstOrDefault(a => a.Username.ToLower() == username.ToLower());

    public AuthUser? GetByCustomerId(int customerId) =>
        db.AuthUsers.FirstOrDefault(a => a.CustomerId == customerId && a.Role == "Customer");

    public AuthUser Add(AuthUser authUser)
    {
        db.AuthUsers.Add(authUser);
        db.SaveChanges();
        return authUser;
    }

    public void Update(AuthUser authUser)
    {
        var existing = db.AuthUsers.FirstOrDefault(a => a.Id == authUser.Id)
            ?? throw new InvalidOperationException("Auth user not found");

        existing.Username = authUser.Username;
        existing.PasswordHash = authUser.PasswordHash;
        existing.Role = authUser.Role;
        existing.RequirePasswordChange = authUser.RequirePasswordChange;
        existing.CustomerId = authUser.CustomerId;
        db.SaveChanges();
    }

    public void DeleteByCustomerId(int customerId)
    {
        var authUsers = db.AuthUsers.Where(a => a.CustomerId == customerId).ToList();
        if (authUsers.Count == 0)
        {
            return;
        }

        db.AuthUsers.RemoveRange(authUsers);
        db.SaveChanges();
    }
}
