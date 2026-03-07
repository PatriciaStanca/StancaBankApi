namespace StancaBankApi.Core.Interfaces;

public interface IJwtHelper
{
    string GenerateToken(string subjectId, string username, string role);
}
