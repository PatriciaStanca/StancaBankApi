namespace StancaBankApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool TryGetCustomerId(this ClaimsPrincipal user, out int customerId)
    {
        var raw = user.FindFirstValue(JwtRegisteredClaimNames.Sub)
                  ?? user.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(raw, out customerId);
    }
}
