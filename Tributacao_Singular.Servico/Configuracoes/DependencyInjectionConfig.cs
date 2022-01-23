using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tributacao_Singular.Aplicacao.Comandos;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Infra.Contexto;
using Tributacao_Singular.Infra.Repositorios;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Servico.Extensoes;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Servico.Configuracoes
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Contextos
            services.AddScoped<MeuDbContext>();
            services.AddScoped<ApplicationDbContext>();

            //Identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            //Mediator(BuS)
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Eventos e notificacoes
            services.AddScoped<INotificationHandler<NotificacaoDominio>, NotificacaoDominioHandler>();

            //Comandos
            services.AddScoped<IRequestHandler<AdicionarClienteComando, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverClienteComando, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarClienteComando, bool>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarProdutoClienteComando, bool>, ClienteCommandHandler>();

            //Servicos
            services.AddScoped<IClienteServicoApp, ClienteServicoApp>();

            //Repositorio
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            return services;
        }
    }
}
