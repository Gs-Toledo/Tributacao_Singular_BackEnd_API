using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;
using Vir_Fundos_Infraestrutura.Mensagens.Eventos;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Vir_Fundos_Infraestrutura.Comunicacao.Mediador
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Evento;
        Task<bool> EnviarComando<T>(T comando) where T : Comando;
        Task PublicarNotificacao<T>(T notificacao) where T : NotificacaoDominio;
        Task PublicarDomainEvent<T>(T notificacao) where T : EventoDominio;
    }
}
