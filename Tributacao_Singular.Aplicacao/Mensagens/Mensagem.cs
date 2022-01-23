using System;
using System.Collections.Generic;
using System.Text;

namespace Vir_Fundos_Infraestrutura.Mensagens
{
    public abstract class Mensagem
    {
        public string Tipo { get; protected set; }
        public string AgragacaoId { get; protected set; }

        protected Mensagem()
        {
            Tipo = GetType().Name;
        }
    }
}
