namespace StancaBankApi.Core.Services;

public class CustomerService(
    ICustomerRepo customerRepo,
    IAuthUserRepo authUserRepo,
    IPasswordService passwordService,
    IJwtHelper jwtHelper) : ICustomerService
{
    private static CustomerDTO ToCustomerDto(Customer customer, string emailAddress) => new()
    {
        Id = customer.Id,
        EmailAddress = emailAddress,
        GivenName = customer.GivenName,
        Surname = customer.Surname
    };

    private static string GetLoginIdentifier(LoginDTO dto) =>
        string.IsNullOrWhiteSpace(dto.EmailAddress) ? (dto.Username ?? string.Empty) : dto.EmailAddress;

    public string? Login(LoginDTO dto)
    {
        var authUser = authUserRepo.GetByUsername(GetLoginIdentifier(dto));
        if (authUser is null || authUser.Role != "Customer" || !passwordService.Verify(dto.Password, authUser.PasswordHash))
        {
            return null;
        }

        var customerId = authUser.CustomerId
            ?? throw new InvalidOperationException("Customer login saknar CustomerId.");

        return jwtHelper.GenerateToken(customerId.ToString(), authUser.Username, "Customer");
    }

    public void ChangeOwnPassword(int customerId, ChangePasswordDto dto)
    {
        var authUser = authUserRepo.GetByCustomerId(customerId)
            ?? throw new ArgumentException("Kundkonto hittades inte.");

        if (!passwordService.Verify(dto.CurrentPassword, authUser.PasswordHash))
        {
            throw new InvalidOperationException("Nuvarande lösenord är fel.");
        }

        authUser.PasswordHash = passwordService.HashPassword(dto.NewPassword);
        authUserRepo.Update(authUser);
    }

    public CustomerDTO? GetById(int id)
    {
        var customer = customerRepo.GetById(id);
        var authUser = authUserRepo.GetByCustomerId(id);
        if (customer is null || authUser is null)
        {
            return null;
        }

        return ToCustomerDto(customer, authUser.Username);
    }

    public List<CustomerDTO> GetAll() =>
        customerRepo.GetAll()
            .Select(c =>
            {
                var authUser = authUserRepo.GetByCustomerId(c.Id);
                return authUser is null ? null : ToCustomerDto(c, authUser.Username);
            })
            .Where(dto => dto is not null)
            .Cast<CustomerDTO>()
            .ToList();
}
