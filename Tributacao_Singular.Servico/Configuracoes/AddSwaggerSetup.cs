using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace Tributacao_Singular.Servico.Configuracoes
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "Tributacao Singular",
                    Description = "Api para tributação de produtos",
                    Contact = new OpenApiContact { Name = "TributacaoSingular", Email = "jpsribeiro@id.uff.br", Url = new Uri("https://TributacaoSingular.com.br") },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://TributacaoSingular.com.br") },
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando o esquema portador. \r\n\r\n 
                      Digite 'Bearer' [espaço] e em seguida, o seu token na entrada de texto abaixo.
                      \r\n\r\nExemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TributacaoSingular");
            });
        }
    }
}
