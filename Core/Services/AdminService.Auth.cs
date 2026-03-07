namespace StancaBankApi.Core.Services;

public partial class AdminService
{
    public string? Login(AdminLoginDto dto)
    {
        var admin = adminRepo.GetByUsername(dto.Username.Trim());
        if (admin is null || !passwordService.Verify(dto.Password, admin.PasswordHash))
        {
            return null;
        }

        return jwtHelper.GenerateToken(admin.Id.ToString(), admin.Username, "Admin");
    }

    public void ChangeOwnPassword(string username, ChangePasswordDto dto)
    {
        var admin = adminRepo.GetByUsername(username.Trim())
            ?? throw new ArgumentException("Admin-konto hittades inte.");

        if (!passwordService.Verify(dto.CurrentPassword, admin.PasswordHash))
        {
            throw new InvalidOperationException("Nuvarande lösenord är fel.");
        }

        admin.PasswordHash = passwordService.HashPassword(dto.NewPassword);
        authUserRepo.Update(admin);
    }
}
