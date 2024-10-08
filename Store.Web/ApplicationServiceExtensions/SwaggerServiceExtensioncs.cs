using Microsoft.OpenApi.Models;

namespace Store.Web.ApplicationServiceExtensions
{
    public static class SwaggerServiceExtensioncs
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Store",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Asmaa Shaker",
                        Email = "en.asmaashaker@gmail.com"
                    }
                });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description="Jwt Desription",
                    Name="Authorization",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="bearer",
                    Reference = new OpenApiReference 
                    {
                        Id="bearer" ,
                        Type=ReferenceType.SecurityScheme
                    }
                    
                };
                options.AddSecurityDefinition("bearer",securityScheme);
                var securityRecuirments = new OpenApiSecurityRequirement
                {
                    {securityScheme,new [] {"bearer"}}
                };
                options.AddSecurityRequirement(securityRecuirments);
            }); 
            return services;
        }
    }
}
