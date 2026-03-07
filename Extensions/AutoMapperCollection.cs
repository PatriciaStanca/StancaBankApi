namespace StancaBankApi.Extensions;

public static class AutoMapperCollection
{
    public static IServiceCollection AddAutoMapperCollection(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StancaBankApi.Data.Profiles.AccountProfile).Assembly);
        return services;
    }
}
