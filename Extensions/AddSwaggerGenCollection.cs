namespace StancaBankApi.Extensions;

public static class AddSwaggerGenCollection
{
    public static IServiceCollection AddSwaggerGenCollectionServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "StancaBankApi", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ange JWT token: Bearer {token}"
            });

            c.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }
}
