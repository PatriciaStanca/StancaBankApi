namespace StancaBankApi.Core.Services;

public partial class AdminService
{
    public CustomerDTO CreateCustomer(CreateCustomerDTO dto)
    {
        if (authUserRepo.GetByUsername(dto.EmailAddress) is not null)
        {
            throw new InvalidOperationException("Användarnamnet är redan upptaget.");
        }

        var customer = customerRepo.GetById(dto.CustomerId)
            ?? throw new ArgumentException("Kunden finns inte i Customers.");

        authUserRepo.Add(new AuthUser
        {
            CustomerId = customer.Id,
            Username = dto.EmailAddress,
            PasswordHash = passwordService.HashPassword(dto.Password),
            Role = "Customer",
            RequirePasswordChange = false
        });

        if (!string.Equals(customer.EmailAddress, dto.EmailAddress, StringComparison.OrdinalIgnoreCase))
        {
            customer.EmailAddress = dto.EmailAddress;
            customerRepo.Update(customer);
        }

        return ToCustomerDto(customer, dto.EmailAddress);
    }

    public CustomerDTO UpdateCustomer(int customerId, UpdateCustomerDTO dto)
    {
        var customer = customerRepo.GetById(customerId)
            ?? throw new ArgumentException("Kunden finns inte.");

        var authUser = authUserRepo.GetByCustomerId(customerId)
            ?? throw new ArgumentException("Inloggningskonto saknas för kunden.");

        var usernameExists = authUserRepo.GetByUsername(dto.EmailAddress);
        if (usernameExists is not null && usernameExists.Id != authUser.Id)
        {
            throw new InvalidOperationException("Användarnamnet är redan upptaget.");
        }

        customer.GivenName = dto.GivenName;
        customer.Surname = dto.Surname;
        customer.EmailAddress = dto.EmailAddress;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            authUser.PasswordHash = passwordService.HashPassword(dto.Password);
        }

        authUser.Username = dto.EmailAddress;
        customerRepo.Update(customer);
        authUserRepo.Update(authUser);
        return ToCustomerDto(customer, authUser.Username);
    }

    public void DeleteCustomer(int customerId)
    {
        var customer = customerRepo.GetById(customerId)
            ?? throw new ArgumentException("Kunden finns inte.");

        var accounts = accountRepo.GetByCustomerId(customer.Id);
        if (accounts.Any(a => a.Balance != 0m))
        {
            throw new InvalidOperationException("Kunden har konton med saldo. Töm konton innan borttagning.");
        }

        var accountIds = accounts.Select(a => a.Id).ToList();
        loanRepo.DeleteByCustomerId(customer.Id);
        transactionRepo.DeleteByAccountIds(accountIds);
        dispositionRepo.DeleteByCustomerId(customer.Id);
        accountRepo.DeleteByCustomerId(customer.Id);
        authUserRepo.DeleteByCustomerId(customer.Id);
        customerRepo.Delete(customer.Id);
    }
}
