using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Vir_Fundos_Infraestrutura.Mensagens.Eventos
{
    [ExcludeFromCodeCoverage]
    public abstract class EventoDominio : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected EventoDominio(string aggregateId)
        {
            AgragacaoId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
