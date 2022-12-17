using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Vir_Fundos_Infraestrutura.Mensagens.Notificacao
{
    [ExcludeFromCodeCoverage]
    public class NotificacaoDominio : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid NotificacaoDominioId { get; private set; }
        public string Chave { get; private set; }
        public string Valor { get; private set; }
        public int Versao { get; private set; }

        public NotificacaoDominio(string chave, string valor)
        {
            Timestamp = DateTime.Now;
            NotificacaoDominioId = Guid.NewGuid();
            Versao = 1;
            Chave = chave;
            Valor = valor;
        }
    }
}
