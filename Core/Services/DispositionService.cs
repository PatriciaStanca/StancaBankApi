namespace StancaBankApi.Core.Services;

public class DispositionService(IDispositionRepo dispositionRepo) : IDispositionService
{
    public List<DispositionDTO> GetByCustomerId(int customerId) =>
        dispositionRepo.GetByCustomerId(customerId)
            .Select(d => new DispositionDTO
            {
                Id = d.Id,
                CustomerId = d.CustomerId,
                AccountId = d.AccountId,
                Role = d.Role
            })
            .ToList();
}
