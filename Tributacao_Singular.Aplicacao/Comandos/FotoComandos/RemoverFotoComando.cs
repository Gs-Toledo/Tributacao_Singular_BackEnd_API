using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.FotoComandos
{
    public class RemoverFotoComando : Comando
    {
        public Guid Id { get; set; }

        public RemoverFotoComando(Guid id)
        {
            Id = id;
        }
    }
}
