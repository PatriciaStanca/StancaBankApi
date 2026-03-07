namespace StancaBankApi.Core.Services;

public partial class AdminService : IAdminService
{
    private readonly IAdminRepo adminRepo;
    private readonly IAuthUserRepo authUserRepo;
    private readonly ICustomerRepo customerRepo;
    private readonly IAccountRepo accountRepo;
    private readonly IAccountTypeRepo accountTypeRepo;
    private readonly IDispositionRepo dispositionRepo;
    private readonly ILoanRepo loanRepo;
    private readonly ITransactionRepo transactionRepo;
    private readonly IPasswordService passwordService;
    private readonly IJwtHelper jwtHelper;

    public AdminService(
        IAdminRepo adminRepo,
        IAuthUserRepo authUserRepo,
        ICustomerRepo customerRepo,
        IAccountRepo accountRepo,
        IAccountTypeRepo accountTypeRepo,
        IDispositionRepo dispositionRepo,
        ILoanRepo loanRepo,
        ITransactionRepo transactionRepo,
        IPasswordService passwordService,
        IJwtHelper jwtHelper)
    {
        this.adminRepo = adminRepo;
        this.authUserRepo = authUserRepo;
        this.customerRepo = customerRepo;
        this.accountRepo = accountRepo;
        this.accountTypeRepo = accountTypeRepo;
        this.dispositionRepo = dispositionRepo;
        this.loanRepo = loanRepo;
        this.transactionRepo = transactionRepo;
        this.passwordService = passwordService;
        this.jwtHelper = jwtHelper;
    }

    private CustomerDTO ToCustomerDto(Customer customer, string emailAddress) => new()
    {
        Id = customer.Id,
        EmailAddress = emailAddress,
        GivenName = customer.GivenName,
        Surname = customer.Surname
    };
}
