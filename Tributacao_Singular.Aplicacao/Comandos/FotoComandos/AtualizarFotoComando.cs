using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.FotoComandos
{
    public class AtualizarFotoComando : Comando
    {
        public Guid Id { get; set; }

        public byte[] Src { get; set; }

        public Guid idUsuario { get; set; }

        public AtualizarFotoComando(Guid id, byte[] Src, Guid idUsuario)
        {
            Id = id;
            this.Src = Src;
            this.idUsuario = idUsuario;
        }
    }
}
