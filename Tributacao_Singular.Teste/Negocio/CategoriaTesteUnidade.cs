using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;
using Tributacao_Singular.Negocio.Validacoes;

namespace Tributacao_Singular.Teste.Negocio
{
    public class CategoriaTesteUnidade : IClassFixture<Inicializar>
    {

        [Fact]
        public void DeveGerarCategoria()
        {
            Categoria categoria = new Categoria();

            Assert.True(categoria.descricao.Equals(String.Empty));
        }

        [Fact]
        public void DeveGerarCategoriaSemErro() 
        {
            Categoria categoria = new Categoria("Descricao",10,10,10,new List<Produto>());

            var validator = new CategoriasValidation();

            Assert.True(validator.Validate(categoria).IsValid);
        }

        [Fact]
        public void DeveGerarCategoriaComErroDescricao()
        {
            Categoria categoria = new Categoria("A", 10, 10, 10, new List<Produto>());

            var validator = new CategoriasValidation();

            var result = validator.Validate(categoria);

            Assert.Contains(result.Errors, o => o.PropertyName == "descricao");
        }

        [Fact]
        public void DeveGerarCategoriaComErroICMS()
        {
            Categoria categoria = new Categoria();
            categoria.descricao = "descricao";
            categoria.Produtos = new List<Produto>();
            categoria.Cofins = 10;
            categoria.IPI = 10;

            var validator = new CategoriasValidation();

            var result = validator.Validate(categoria);

            Assert.Contains(result.Errors, o => o.PropertyName == "ICMS");
        }

        [Fact]
        public void DeveGerarCategoriaComErroIPI()
        {
            Categoria categoria = new Categoria();
            categoria.descricao = "descricao";
            categoria.Produtos = new List<Produto>();
            categoria.Cofins = 10;
            categoria.ICMS = 10;

            var validator = new CategoriasValidation();

            var result = validator.Validate(categoria);

            Assert.Contains(result.Errors, o => o.PropertyName == "IPI");
        }

        [Fact]
        public void DeveGerarCategoriaComErroCofins()
        {
            Categoria categoria = new Categoria();
            categoria.descricao = "descricao";
            categoria.Produtos = new List<Produto>();
            categoria.IPI = 10;
            categoria.ICMS = 10;

            var validator = new CategoriasValidation();

            var result = validator.Validate(categoria);

            Assert.Contains(result.Errors, o => o.PropertyName == "Cofins");
        }
    }
}
