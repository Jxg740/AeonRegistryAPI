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

                string[] hiddenEndpoints = [
                    "api/auth/register",
                    "api/auth/refresh",
                    "api/auth/confirmemail",
                    "api/auth/resendconfirmationemail",
                    "api/auth/forgotpassword",
                    "api/auth/resetpassword",
                    "api/auth/manage",
                    "api/auth/manage/info",
                    "api/auth/manage/2fa"
                    ];

                
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var path = apiDesc.RelativePath?.ToLowerInvariant();

                    if (path is null)
                    {
                        return false;
                    }
                    if (hiddenEndpoints.Contains(path, StringComparer.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    return true;

                });

            });

            return services;
        }
    }
}
