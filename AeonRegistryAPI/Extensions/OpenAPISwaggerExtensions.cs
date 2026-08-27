using Microsoft.OpenApi;
using System.Reflection.Metadata;

namespace AeonRegistryAPI.Extensions
{
    public static class OpenAPISwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Aeo Registry API",
                    Version = "v1",
                    Description = """

                    <img src="/images/AeonRegistryLogo.png" height="120" />

                    ## Aeon Research Division

                    Internal API managing recovered artifacts and research data.
                    Provides secure access for field researchers and analysts.


                    ### Key Features
                    - Site and Artifacts Cataloging
                    - Research record submissions
                    - Secure media storage
                    - User role management

                    """,
                    Contact = new OpenApiContact
                    {
                        Name = "Aeon Registry Team",
                        Url = new Uri("https://github.com/Jxg740/AeonRegistryAPI"),
                        Email = "jgierke@yellowstonecountymt.gov"
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {

                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid JWT token."

                });

                c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
                {                    
                    [new OpenApiSecuritySchemeReference("bearer", doc)] = []
                });

            });

            return services;
        }
    }
}
