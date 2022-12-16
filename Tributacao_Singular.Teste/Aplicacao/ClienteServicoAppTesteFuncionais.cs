using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class ClienteServicoAppTesteFuncionais : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public ClienteServicoAppTesteFuncionais(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        [Fact]
        public async Task ObterPorIdTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            var repositorioMock = new Mock<IClienteRepositorio>();

            var cliente = new Cliente();

            repositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(cliente);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ObterPorIdAsync(It.IsAny<Guid>());

            Assert.True(lista != null);
        }

        [Fact]
        public async Task ObterPorClienteProdutosPorIdTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            var repositorioMock = new Mock<IClienteRepositorio>();

            var cliente = new Cliente();

            repositorioMock
                .Setup(x => x.ObterClienteProdutosPorId(It.IsAny<Guid>()))
                .ReturnsAsync(cliente);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ObterClienteProdutosPorIdAsync(It.IsAny<Guid>());

            Assert.True(lista != null);
        }

        [Fact]
        public async Task ObterPorClienteProdutosPorCnpjTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            var repositorioMock = new Mock<IClienteRepositorio>();

            var cliente = new Cliente();

            repositorioMock
                .Setup(x => x.ObterClienteProdutosPorCnpj(It.IsAny<String>()))
                .ReturnsAsync(cliente);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ObterClienteProdutosPorCnpjAsync(It.IsAny<String>());

            Assert.True(lista != null);
        }

        [Fact]
        public async Task ListarTodosClientesProdutosTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<IEnumerable<ClienteViewModel>>(It.IsAny<List<Cliente>>()))
                .Returns(new List<ClienteViewModel>
                {
                    new ClienteViewModel()
                });

            var repositorioMock = new Mock<IClienteRepositorio>();

            var categorias = new List<Cliente>()
            {
                new Cliente()
            };

            repositorioMock
                .Setup(x => x.ObterTodosClienteProdutos())
                .ReturnsAsync(categorias);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ObterTodosClienteProdutosAsync();

            Assert.True(lista.Any());
        }

        [Fact]
        public async Task ListarTodosTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<List<ClienteViewModel>>(It.IsAny<List<Cliente>>()))
                .Returns(new List<ClienteViewModel>
                {
                    new ClienteViewModel()
                });

            var repositorioMock = new Mock<IClienteRepositorio>();

            var categorias = new List<Cliente>()
            {
                new Cliente()
            };

            repositorioMock
                .Setup(x => x.ObterTodos())
                .ReturnsAsync(categorias);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ListarTodosAsync();

            Assert.True(lista.Any());
        }

        [Fact]
        public async Task DeveAdicionarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IClienteRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<IEnumerable<ProdutoViewModel>>(It.IsAny<IEnumerable<Produto>>()))
                .Returns((IEnumerable<Produto> src) => new List<ProdutoViewModel>() { new ProdutoViewModel() });

            mapperMock
                .Setup(m => m.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns((Cliente src) => new ClienteViewModel()
                {
                    cnpj = src.cnpj,
                    Id = src.Id,
                    nome = src.nome,
                    Produtos = mapperMock.Object.Map<List<ProdutoViewModel>>(src.Produtos)
                });

            mapperMock
                .Setup(m => m.Map<AdicionarClienteComando>(It.IsAny<ClienteViewModel>()))
                .Returns((ClienteViewModel src) => new AdicionarClienteComando(src.Id, src.cnpj,src.Produtos, src.nome));

            repositorioCategoriaMock
                .Setup(c => c.Adicionar(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            ClienteServicoApp servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.AdicionarAsync(mapperMock.Object.Map<ClienteViewModel>(new Cliente("Cliente",new List<Produto>(), "77.213.645/0001-37")));

            Assert.True(adicionou);
        }

        [Fact]
        public async Task DeveAtualizarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IClienteRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<IEnumerable<ProdutoViewModel>>(It.IsAny<IEnumerable<Produto>>()))
                .Returns((IEnumerable<Produto> src) => new List<ProdutoViewModel>() { new ProdutoViewModel() });

            mapperMock
                .Setup(m => m.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns((Cliente src) => new ClienteViewModel()
                {
                    cnpj = src.cnpj,
                    Id = src.Id,
                    nome = src.nome,
                    Produtos = mapperMock.Object.Map<List<ProdutoViewModel>>(src.Produtos)
                });

            mapperMock
                .Setup(m => m.Map<AtualizarClienteComando>(It.IsAny<ClienteViewModel>()))
                .Returns((ClienteViewModel src) => new AtualizarClienteComando(src.Id, src.cnpj, src.Produtos, src.nome));

            repositorioCategoriaMock
                .Setup(c => c.Atualizar(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            ClienteServicoApp servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var atualizou = await servico.AtualizarAsync(mapperMock.Object.Map<ClienteViewModel>(new Cliente("Cliente",new List<Produto>(), "77.213.645/0001-37")));

            Assert.True(atualizou);
        }

        [Fact]
        public async Task DeveRemoverTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IClienteRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<IEnumerable<ProdutoViewModel>>(It.IsAny<IEnumerable<Produto>>()))
                .Returns((IEnumerable<Produto> src) => new List<ProdutoViewModel>() { new ProdutoViewModel() });

            mapperMock
                .Setup(m => m.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns((Cliente src) => new ClienteViewModel()
                {
                    cnpj = src.cnpj,
                    Id = src.Id,
                    nome = src.nome,
                    Produtos = mapperMock.Object.Map<List<ProdutoViewModel>>(src.Produtos)
                });

            mapperMock
                .Setup(m => m.Map<RemoverClienteComando>(It.IsAny<ClienteViewModel>()))
                .Returns((ClienteViewModel src) => new RemoverClienteComando(src.Id));

            repositorioCategoriaMock
                .Setup(c => c.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            ClienteServicoApp servico = new ClienteServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var removeu = await servico.RemoverAsync(It.IsAny<Guid>());

            Assert.True(removeu);
        }
    }
}
