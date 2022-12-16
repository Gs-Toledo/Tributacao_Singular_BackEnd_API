using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Modelos
{
    public class Categoria : Entity
    {
        public string descricao { get; set; }

        public decimal ICMS { get; set; }

        public decimal Cofins { get; set; }

        public decimal IPI { get; set; }

        public IEnumerable<Produto> Produtos {get; set;}

        public Categoria()
        {
            this.descricao = String.Empty;
            this.Produtos = new List<Produto>();
        }

        public Categoria(string descricao, decimal iCMS, decimal cofins, decimal iPI, List<Produto> produtos)
        {
            this.descricao = descricao;
            this.ICMS = iCMS;
            this.Cofins = cofins;
            this.IPI = iPI;
            this.Produtos = produtos;
        }
    }
}
