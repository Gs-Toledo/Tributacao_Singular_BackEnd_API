using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Vir_Fundos_Infraestrutura.Mensagens.Eventos
{
    [ExcludeFromCodeCoverage]
    public abstract class Evento : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Evento()
        {
            Timestamp = DateTime.Now;
        }
    }
}
