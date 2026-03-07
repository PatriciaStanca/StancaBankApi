namespace StancaBankApi.Data.Profiles;

public class LoanProfile : Profile
{
    public LoanProfile()
    {
        CreateMap<Loan, LoanDTO>();
    }
}
