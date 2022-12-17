using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Comandos;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;
using System.Linq.Expressions;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class ClienteCommandHandlerTeste : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public ClienteCommandHandlerTeste(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        #region Comandos

        [Fact]
        public void DeveGerarAdicionarClienteComando()
        {
            AdicionarClienteComando adicionarClienteComando = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            Assert.True(adicionarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarClienteErroNomeComando()
        {
            AdicionarClienteComando adicionarClienteComando = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), It.IsAny<String>());

            Assert.False(adicionarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarClienteErroCNPJComando()
        {
            AdicionarClienteComando adicionarClienteComando = new AdicionarClienteComando(Guid.NewGuid(), It.IsAny<String>(), new List<ProdutoViewModel>(), "nome");

            Assert.False(adicionarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarClienteComando()
        {
            AtualizarClienteComando atualizarClienteComando = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            Assert.True(atualizarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarClienteErroNomeComando()
        {
            AtualizarClienteComando atualizarClienteComando = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), It.IsAny<String>());

            Assert.False(atualizarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarClienteErroCNPJComando()
        {
            AtualizarClienteComando atualizarClienteComando = new AtualizarClienteComando(Guid.NewGuid(), It.IsAny<String>(), new List<ProdutoViewModel>(), "nome");

            Assert.False(atualizarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarRemoverClienteComando()
        {
            RemoverClienteComando adicionarClienteComando = new RemoverClienteComando(Guid.NewGuid());

            Assert.True(adicionarClienteComando.EhValido());
        }

        [Fact]
        public void DeveGerarRemoverClienteErroId()
        {
            RemoverClienteComando adicionarClienteComando = new RemoverClienteComando(It.IsAny<Guid>());

            Assert.False(adicionarClienteComando.EhValido());
        }

        #endregion

        #region AdicionarHandler
        [Fact]
        public async Task DeveLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAdicionarInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), It.IsAny<String>());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
        }

        [Fact]
        public async Task DeveLidarComAdicionarClienteExistenteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.ObterPorCnpj(It.IsAny<String>()))
                .ReturnsAsync(new Cliente());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Cliente>()))
                .ThrowsAsync(new DominioException());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Cliente>()))
                .ThrowsAsync(new Exception());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        #endregion

        #region AtualizarHandler
        [Fact]
        public async Task DeveLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.ObterClienteProdutosPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), It.IsAny<String>());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
        }

        [Fact]
        public async Task DeveLidarComAtualizarClienteComIdInexistenteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarClienteComCnpjExistenteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.ObterClienteProdutosPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente());

            clienteRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Cliente, bool>>>()))
                .ReturnsAsync(new List<Cliente>() { new Cliente() });

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.ObterClienteProdutosPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente());

            clienteRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Cliente>()))
                .ThrowsAsync(new DominioException());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.ObterClienteProdutosPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente());

            clienteRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Cliente>()))
                .ThrowsAsync(new Exception());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarClienteComando(Guid.NewGuid(), "80.455.619/0001-20", new List<ProdutoViewModel>(), "nome");

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        #endregion

        #region RemoverHandler

        [Fact]
        public async Task DeveLidarComRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new RemoverClienteComando(Guid.NewGuid());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComRemoverComProdutosAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterProdutosPorClienteId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Produto>()
                {   new Produto("descricao", "11111111", "1111111111111", new Categoria(), new Cliente(), 0),
                    new Produto("descricao", "22222222", "2222222222222", new Categoria(), new Cliente(), 0),
                    new Produto("descricao", "33333333", "3333333333333", new Categoria(), new Cliente(), 0)
                });

            produtoRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Returns(() => Task.CompletedTask);

            var command = new RemoverClienteComando(Guid.NewGuid());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComRemoverInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new RemoverClienteComando(It.IsAny<Guid>());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .ThrowsAsync(new DominioException());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new RemoverClienteComando(Guid.NewGuid());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ClienteViewModel>(It.IsAny<Cliente>()))
                .Returns(new ClienteViewModel());

            mapperMock
                .Setup(x => x.Map<Cliente>(It.IsAny<ClienteViewModel>()))
                .Returns(new Cliente());

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new RemoverClienteComando(Guid.NewGuid());

            var handler = new ClienteCommandHandler(mediatorMock.Object, clienteRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
        }

        #endregion
    }
}
