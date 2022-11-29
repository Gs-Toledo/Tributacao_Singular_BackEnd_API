using FluentValidation;
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

            var validator = new ClienteValidation();

            Assert.True(validator.Validate(cliente).IsValid);
        }

        [Fact]
        public void DeveGerarClienteComErroNome()
        {
            Cliente cliente = new Cliente("N", new List<Produto>(), "80.455.619/0001-20");

            var validator = new ClienteValidation();

            var result = validator.Validate(cliente);

            Assert.Contains(result.Errors, o => o.PropertyName == "nome");
        }
    }
}
