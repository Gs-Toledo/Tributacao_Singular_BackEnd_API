using MediatR;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vir_Fundos_Infraestrutura.Mensagens.Notificacao
{
    [ExcludeFromCodeCoverage]
    public class NotificacaoDominioHandler : INotificationHandler<NotificacaoDominio>
    {
        private List<NotificacaoDominio> notificacoes;

        public NotificacaoDominioHandler()
        {
            this.notificacoes = new List<NotificacaoDominio>();
        }

        public Task Handle(NotificacaoDominio message, CancellationToken cancellationToken)
        {
            notificacoes.Add(message);
            return Task.CompletedTask;
        }

        public virtual List<NotificacaoDominio> ObterNotificacoes()
        {
            return notificacoes;
        }

        public virtual bool TemNotificacao()
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose()
        {
            notificacoes = new List<NotificacaoDominio>();
        }
    }
}
