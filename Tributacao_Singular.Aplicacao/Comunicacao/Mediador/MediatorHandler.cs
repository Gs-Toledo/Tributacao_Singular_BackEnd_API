using MediatR;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;
using Vir_Fundos_Infraestrutura.Mensagens.Eventos;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Vir_Fundos_Infraestrutura.Comunicacao.Mediador
{
    [ExcludeFromCodeCoverage]
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediador;

        public MediatorHandler(IMediator mediator)
        {
            mediador = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Comando
        {
            return await mediador.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Evento
        {
            await mediador.Publish(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : NotificacaoDominio
        {
            await mediador.Publish(notificacao);
        }

        public async Task PublicarDomainEvent<T>(T notificacao) where T : EventoDominio
        {
            await mediador.Publish(notificacao);
        }
    }
}
