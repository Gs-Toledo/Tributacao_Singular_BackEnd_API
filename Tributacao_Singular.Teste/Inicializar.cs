using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tributacao_Singular.Servico.Configuracoes;

namespace Tributacao_Singular.Teste
{
    public class Inicializar
    {
        public ServiceProvider ProvedorServico { get; private set; }
        public IConfiguration configuracao { get; }

        public Inicializar()
        {
            var servicos = new ServiceCollection();
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true);

            builder.AddEnvironmentVariables();
            configuracao = builder.Build();

            servicos.AddAutoMapperSetup();
            servicos.AddMediatR(typeof(Inicializar));

            RegistrarServicos(servicos);

            ProvedorServico = servicos.BuildServiceProvider();
        }

        private static void RegistrarServicos(IServiceCollection servicos)
        {
            DependencyInjectionConfig.ResolveDependencies(servicos);
        }
    }
}
