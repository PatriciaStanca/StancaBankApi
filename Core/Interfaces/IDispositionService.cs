namespace StancaBankApi.Core.Interfaces;

public interface IDispositionService
{
    List<DispositionDTO> GetByCustomerId(int customerId);
}
