using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public  class ProdutoCommandHandlerTeste : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public ProdutoCommandHandlerTeste(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        #region Comandos
        [Fact]
        public void DeveGerarAdicionarProdutoComando()
        {
            AdicionarProdutoComando adicionarProdutosComando = new AdicionarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", "10", 1, Guid.NewGuid());

            Assert.True(adicionarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarProdutoErroDescricaoComando()
        {
            AdicionarProdutoComando adicionarProdutosComando = new AdicionarProdutoComando(Guid.NewGuid(), It.IsAny<String>(), "10", "10", 1, Guid.NewGuid());

            Assert.False(adicionarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarProdutoErroNcmComando()
        {
            AdicionarProdutoComando adicionarProdutosComando = new AdicionarProdutoComando(Guid.NewGuid(), "Descricao do produto", It.IsAny<String>(), "10", 1, Guid.NewGuid());

            Assert.False(adicionarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarProdutoErroEanComando()
        {
            AdicionarProdutoComando adicionarProdutosComando = new AdicionarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", It.IsAny<String>(), 1, Guid.NewGuid());

            Assert.False(adicionarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarProdutoErroDescricaoComando()
        {
            AtualizarProdutoComando atualizarProdutosComando = new AtualizarProdutoComando(Guid.NewGuid(), It.IsAny<String>(), "10", "10", 1, Guid.NewGuid());

            Assert.False(atualizarProdutosComando.EhValido());
        }


        [Fact]
        public void DeveGerarAtualizarProdutoErroNcmComando()
        {
            AtualizarProdutoComando atualizarProdutosComando = new AtualizarProdutoComando(Guid.NewGuid(), "Descricao do produto", It.IsAny<String>(), "10", 1, Guid.NewGuid());

            Assert.False(atualizarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarProdutoErroEanComando()
        {
            AtualizarProdutoComando atualizarProdutosComando = new AtualizarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", It.IsAny<String>(), 1, Guid.NewGuid());

            Assert.False(atualizarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarProdutoErroStatusComando()
        {
            AtualizarProdutoComando atualizarProdutosComando = new AtualizarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", "10", It.IsAny<int>(), Guid.NewGuid());

            Assert.False(atualizarProdutosComando.EhValido());
        }

        [Fact]
        public void DeveGerarRemoverProdutoComando()
        {
            RemoverProdutoComando removerCategoriaComando = new RemoverProdutoComando(Guid.NewGuid());

            Assert.True(removerCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarRemoverProdutoErroId()
        {
            RemoverProdutoComando removerCategoriaComando = new RemoverProdutoComando(It.IsAny<Guid>());

            Assert.False(removerCategoriaComando.EhValido());
        }

        #endregion

        #region AdicionarHandler
        [Fact]
        public async Task DeveLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            clienteRepositorioMock
                .Setup(x => x.ObterClienteProdutosPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente());

            //categoriaRepositorioMock
            //    .Setup(x => x.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>()))
            //    .ReturnsAsync(new Cliente());

            var command = new AdicionarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);


            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAdicionarInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Produto>(It.IsAny<ProdutoViewModel>()))
                .Returns(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            var command = new AdicionarProdutoComando(Guid.NewGuid(), It.IsAny<String>(), "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Produto>(It.IsAny<ProdutoViewModel>()))
                .Returns(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Produto>()))
                .Throws(new DominioException());

            var command = new AdicionarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));

            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Produto>(It.IsAny<ProdutoViewModel>()))
                .Returns(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Produto>()))
                .Throws(new Exception());

            var command = new AdicionarProdutoComando(Guid.NewGuid(), "Descricao do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

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
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));


            var command = new AtualizarProdutoComando(Guid.NewGuid(), "Descrição do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            var command = new AtualizarProdutoComando(Guid.NewGuid(), It.IsAny<String>(), "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarProdutoNaoExistenteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            var command = new AtualizarProdutoComando(Guid.NewGuid(), "Descrição do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Produto>(It.IsAny<ProdutoViewModel>()))
                .Returns(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));
              

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();
            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            produtoRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Produto>()))
                .Throws(new DominioException());

            var command = new AtualizarProdutoComando(Guid.NewGuid(), "Descrição do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));

            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Produto>(It.IsAny<ProdutoViewModel>()))
                .Returns(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            var mediatorMock = new Mock<IMediatorHandler>();

            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();
            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1));

            produtoRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Produto>()))
                .Throws(new Exception());

            var command = new AtualizarProdutoComando(Guid.NewGuid(), "Descrição do produto", "10", "10", 1, Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

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
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Produto, bool>>>()))
                .ReturnsAsync(new List<Produto>(){ new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1) });

            produtoRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var command = new RemoverProdutoComando(Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveGerarDomainExceptionQuandoLidarComRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Produto, bool>>>()))
                .ReturnsAsync(new List<Produto>() { new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1) });

            produtoRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Throws(new DominioException());

            var command = new RemoverProdutoComando(Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns(new ProdutoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var clienteRepositorioMock = new Mock<IClienteRepositorio>();
            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Produto, bool>>>()))
                .ReturnsAsync(new List<Produto>() { new Produto("Descricao do produto", "10", "10", new Categoria(), new Cliente(), 1) });

            produtoRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Throws(new Exception());

            var command = new RemoverProdutoComando(Guid.NewGuid());

            var handler = new ProdutoCommandHandler(mediatorMock.Object, produtoRepositorioMock.Object, clienteRepositorioMock.Object, mapperMock.Object, categoriaRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        #endregion
    }
}
