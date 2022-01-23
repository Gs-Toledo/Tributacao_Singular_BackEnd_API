using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Modelos
{
    public class Cliente : Entity
    {
        public string nome { get; set; }

        public string cnpj { get; set; }

        /* EF Relation */
        public IEnumerable<Produto> Produtos { get; set; }

        public Cliente()
        {
            this.nome = "";
            this.cnpj = "";
            Produtos = new List<Produto>();
        }

        public Cliente(string nome, List<Produto> produtos, string cnpj)
        {
            this.nome = nome;
            this.cnpj = cnpj;
            Produtos = produtos;
        }
    }
}
