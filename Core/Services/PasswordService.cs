namespace StancaBankApi.Core.Services;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    public bool Verify(string password, string passwordHash) =>
        HashPassword(password.Trim()).Equals(passwordHash.Trim(), StringComparison.OrdinalIgnoreCase);
}
