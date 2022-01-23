using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vir_Fundos_Infraestrutura.Mensagens.Eventos
{
    public abstract class Evento : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Evento()
        {
            Timestamp = DateTime.Now;
        }
    }
}
