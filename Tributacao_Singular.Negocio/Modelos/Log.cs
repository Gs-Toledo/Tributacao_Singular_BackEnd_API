using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Modelos
{
    [ExcludeFromCodeCoverage]
    public class Log : Entity
    {
        public string descricao { get; set; }
        public string usuario { get; set; }
        public DateTime DataCadastro { get; set; }

        public string Tabela { get; set; }

        public Log(string descricao, string usuario, string tabela)
        {
            this.descricao = descricao;
            this.usuario = usuario;
            Tabela = tabela;
        }
    }
}
