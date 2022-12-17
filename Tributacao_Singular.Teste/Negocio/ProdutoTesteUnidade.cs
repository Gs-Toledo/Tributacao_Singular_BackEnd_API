using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;
using Tributacao_Singular.Negocio.Validacoes;

namespace Tributacao_Singular.Teste.Negocio
{
    public class ProdutoTesteUnidade : IClassFixture<Inicializar>
    {
        [Fact]
        public void DeveGerarProdutoSemErro()
        {
            Produto produto = new Produto("descricao", "11111111", "1111111111111", new Categoria(), new Cliente(), 0);

            Assert.True(produto.EhValido());
        }

        [Fact]
        public void DeveGerarProdutoComErroDescricao()
        {
            Produto produto = new Produto("d", "11111111", "1111111111111", new Categoria(), new Cliente(), 0);

            Assert.False(produto.EhValido());
        }

        [Fact]
        public void DeveGerarProdutoComErroEAN()
        {
            Produto produto = new Produto("d", "11111111", "1", new Categoria(), new Cliente(), 0);

            Assert.False(produto.EhValido());
        }

        [Fact]
        public void DeveGerarProdutoComErroNCM()
        {
            Produto produto = new Produto("d", "1", "1111111111111", new Categoria(), new Cliente(), 0);

            Assert.False(produto.EhValido());
        }
    }
}
