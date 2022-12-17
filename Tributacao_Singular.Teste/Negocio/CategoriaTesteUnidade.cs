using Microsoft.Extensions.DependencyInjection;
using Moq;
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
        private readonly ServiceProvider provedorServico;

        public CategoriaTesteUnidade(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        [Fact]
        public void DeveGerarCategoriaSemErro() 
        {
            Categoria categoria = new Categoria("Descricao",10,10,10,new List<Produto>());

            Assert.True(categoria.EhValido());
        }

        [Fact]
        public void DeveGerarCategoriaComErroDescricao()
        {
            Categoria categoria = new Categoria(It.IsAny<String>(), 10, 10, 10, new List<Produto>());

            Assert.False(categoria.EhValido());
        }

        [Fact]
        public void DeveGerarCategoriaComErroICMS()
        {
            Categoria categoria = new Categoria("Descricao", It.IsAny<decimal>(), 10, 10, new List<Produto>());

            Assert.False(categoria.EhValido());
        }

        [Fact]
        public void DeveGerarCategoriaComErroIPI()
        {
            Categoria categoria = new Categoria("Descricao", 10, 10, It.IsAny<decimal>(), new List<Produto>());

            Assert.False(categoria.EhValido());
        }

        [Fact]
        public void DeveGerarCategoriaComErroCofins()
        {
            Categoria categoria = new Categoria("Descricao", 10, It.IsAny<decimal>(), 10, new List<Produto>());

            Assert.False(categoria.EhValido());
        }
    }
}
