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

            var validator = new ProdutoValidation();

            Assert.True(validator.Validate(produto).IsValid);
        }

        [Fact]
        public void DeveGerarProdutoComErroDescricao()
        {
            Produto produto = new Produto("d", "11111111", "1111111111111", new Categoria(), new Cliente(), 0);

            var validator = new ProdutoValidation();

            var result = validator.Validate(produto);

            Assert.Contains(result.Errors, o => o.PropertyName == "descricao");
        }

        [Fact]
        public void DeveGerarProdutoComErroEAN()
        {
            Produto produto = new Produto("d", "11111111", "1", new Categoria(), new Cliente(), 0);

            var validator = new ProdutoValidation();

            var result = validator.Validate(produto);

            Assert.Contains(result.Errors, o => o.PropertyName == "EAN");
        }

        [Fact]
        public void DeveGerarProdutoComErroNCM()
        {
            Produto produto = new Produto("d", "1", "1111111111111", new Categoria(), new Cliente(), 0);

            var validator = new ProdutoValidation();

            var result = validator.Validate(produto);

            Assert.Contains(result.Errors, o => o.PropertyName == "NCM");
        }
    }
}
