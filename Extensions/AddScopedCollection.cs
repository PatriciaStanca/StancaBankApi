namespace StancaBankApi.Extensions;

public static class AddScopedCollection
{
    public static IServiceCollection AddScopedCollectionServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminRepo, AdminRepo>();
        services.AddScoped<IAuthUserRepo, AuthUserRepo>();
        services.AddScoped<ICustomerRepo, CustomerRepo>();
        services.AddScoped<IAccountRepo, AccountRepo>();
        services.AddScoped<IAccountTypeRepo, AccountTypeRepo>();
        services.AddScoped<ITransactionRepo, TransactionRepo>();
        services.AddScoped<ILoanRepo, LoanRepo>();
        services.AddScoped<IDispositionRepo, DispositionRepo>();

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IAccountTypeService, AccountTypeService>();
        services.AddScoped<IDispositionService, DispositionService>();

        return services;
    }
}
