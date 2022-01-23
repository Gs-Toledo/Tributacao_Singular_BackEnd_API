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

        public int Status { get; set; }

        public Guid ClienteId { get; set; }

        public Guid CategoriaId { get; set; }

        /* EF Relation */
        public Categoria Categoria { get; set; }
        public Cliente Cliente { get; set; }

        public Produto() 
        {
            descricao = "";
            NCM = "";
            EAN = "";
            Status = 0;
            Categoria = new Categoria();
            Cliente = new Cliente();
        }

        public Produto(string descricao, string NCM, string EAN, Categoria categoria, Cliente cliente, int status)
        {
            this.descricao = descricao;
            this.NCM = NCM;
            this.NCM = NCM;
            this.Status = status;
            Categoria = categoria;
            Cliente = cliente;
        }
    }
}
