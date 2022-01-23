using Tributacao_Singular.Aplicacao.AutoMapper;
using Tributacao_Singular.Aplicacao.Servicos;

namespace Tributacao_Singular.Servico.Configuracoes
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection servicos)
        {
            if (servicos == null) throw new ArgumentNullException(nameof(servicos));

            servicos.AddAutoMapper(typeof(ClienteServicoApp));

            AutoMapperConfiguracaoTributacao.RegisterMappings();
        }
    }
}
