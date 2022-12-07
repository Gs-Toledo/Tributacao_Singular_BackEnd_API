using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Tributacao_Singular.Aplicacao.Comandos;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Comandos.FotoComandos;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
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

            services.AddScoped<IRequestHandler<AdicionarProdutoComando, bool>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarProdutoComando, bool>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverProdutoComando, bool>, ProdutoCommandHandler>();

            services.AddScoped<IRequestHandler<AtualizarCategoriaComando, bool>, CategoriaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverCategoriaComando, bool>, CategoriaCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarCategoriaComando, bool>, CategoriaCommandHandler>();

            services.AddScoped<IRequestHandler<AtualizarFotoComando, bool>, FotoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverFotoComando, bool>, FotoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarFotoComando, bool>, FotoCommandHandler>();

            //Servicos
            services.AddScoped<IClienteServicoApp, ClienteServicoApp>();
            services.AddScoped<IProdutoServicoApp, ProdutoServicoApp>();
            services.AddScoped<ICategoriaServicoApp, CategoriaServicoApp>();
            services.AddScoped<IFotoServicoApp, FotoServicoApp>();

            //Repositorio
            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IFotoRepositorio, FotoRepositorio>();

            return services;
        }
    }
}
