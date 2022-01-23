using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Modelos
{
    public class Produto : Entity
    {
        public string descricao { get; set; }

        public string NCM { get; set; }

        public string EAN { get; set; }

        /* EF Relation */

        public Guid CategoriaId { get; set; }

        public Categoria Categoria { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }

        public Produto() 
        {
            descricao = "";
            NCM = "";
            EAN = "";
            Categoria = new Categoria();
            Clientes = new List<Cliente>();
        }

        public Produto(string descricao, string NCM, string EAN, Categoria categoria, List<Cliente> clientes)
        {
            this.descricao = descricao;
            this.NCM = NCM;
            this.NCM = NCM;
            Categoria = categoria;
            Clientes = clientes;
        }
    }
}
