using FluentValidation;
using Moq;
using Tributacao_Singular.Negocio.Modelos;
using Tributacao_Singular.Negocio.Validacoes;
using Tributacao_Singular.Negocio.Validacoes.Documentos;

namespace Tributacao_Singular.Teste.Negocio
{
    public class ClienteTesteUnidade : IClassFixture<Inicializar>
    {
        [Fact]
        public void DeveGerarClienteSemErro()
        {
            Cliente cliente = new Cliente("Nome", new List<Produto>(), "80.455.619/0001-20");

            Assert.True(cliente.EhValido());
        }

        [Fact]
        public void DeveGerarClienteComErroNome()
        {
            Cliente cliente = new Cliente(It.IsAny<String>(), new List<Produto>(), "80.455.619/0001-20");

            Assert.False(cliente.EhValido());
        }
    }
}
