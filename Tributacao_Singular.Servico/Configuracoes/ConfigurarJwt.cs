using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tributacao_Singular.Servico.Extensoes;

namespace Tributacao_Singular.Servico.Configuracoes
{
    public static class ConfigurarJwt
    {
        public static void AddJwtConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("ConfiguracaoApp");
            services.Configure<ConfiguracaoApp>(appSettingsSection);

            var configuracaoApp = appSettingsSection.Get<ConfiguracaoApp>();
            var key = Encoding.ASCII.GetBytes(configuracaoApp.Segredo);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuracaoApp.ValidoEm,
                    ValidIssuer = configuracaoApp.Emissor
                };
            });
        }

        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
