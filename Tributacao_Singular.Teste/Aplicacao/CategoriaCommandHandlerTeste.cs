using AutoMapper;
using MediatR;
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
using Tributacao_Singular.Aplicacao.Excecoes;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class CategoriaCommandHandlerTeste : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        
        public CategoriaCommandHandlerTeste(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        #region Comandos

        [Fact]
        public void DeveGerarAdicionarCategoriaComando() 
        {
            AdicionarCategoriaComando adicionarCategoriaComando = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            Assert.True(adicionarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarCategoriaErroDescricaoComando()
        {
            AdicionarCategoriaComando adicionarCategoriaComando = new AdicionarCategoriaComando(Guid.NewGuid(), It.IsAny<String>(), 10, 10, 10);

            Assert.False(adicionarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarCategoriaErroICMComando()
        {
            AdicionarCategoriaComando adicionarCategoriaComando = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", It.IsAny<Decimal>(), 10, 10);

            Assert.False(adicionarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarCategoriaErroConfisComando()
        {
            AdicionarCategoriaComando adicionarCategoriaComando = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", 10, It.IsAny<Decimal>(), 10);

            Assert.False(adicionarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAdicionarCategoriaErroIPIComando()
        {
            AdicionarCategoriaComando adicionarCategoriaComando = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, It.IsAny<Decimal>());

            Assert.False(adicionarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarCategoriaComando()
        {
            AtualizarCategoriaComando atualizarCategoriaComando = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            Assert.True(atualizarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarCategoriaErroDescricaoComando()
        {
            AtualizarCategoriaComando atualizarCategoriaComando = new AtualizarCategoriaComando(Guid.NewGuid(), It.IsAny<String>(), 10, 10, 10);

            Assert.False(atualizarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarCategoriaErroICMComando()
        {
            AtualizarCategoriaComando atualizarCategoriaComando = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", It.IsAny<Decimal>(), 10, 10);

            Assert.False(atualizarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarCategoriaErroConfisComando()
        {
            AtualizarCategoriaComando atualizarCategoriaComando = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, It.IsAny<Decimal>(), 10);

            Assert.False(atualizarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarAtualizarCategoriaErroIPIComando()
        {
            AtualizarCategoriaComando atualizarCategoriaComando = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, It.IsAny<Decimal>());

            Assert.False(atualizarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarRemoverCategoriaComando()
        {
            RemoverCategoriaComando adicionarCategoriaComando = new RemoverCategoriaComando(Guid.NewGuid());

            Assert.True(adicionarCategoriaComando.EhValido());
        }

        [Fact]
        public void DeveGerarRemoverCategoriaErroId()
        {
            RemoverCategoriaComando adicionarCategoriaComando = new RemoverCategoriaComando(It.IsAny<Guid>());

            Assert.False(adicionarCategoriaComando.EhValido());
        }

        #endregion

        #region AdicionarHandler
        [Fact]
        public async Task DeveLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAdicionarInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Categoria>(It.IsAny<CategoriaViewModel>()))
                .Returns(new Categoria());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarCategoriaComando(Guid.NewGuid(), It.IsAny<String>(), 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Categoria>(It.IsAny<CategoriaViewModel>()))
                .Returns(new Categoria());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Categoria>()))
                .Throws(new DominioException());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Categoria>(It.IsAny<CategoriaViewModel>()))
                .Returns(new Categoria());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Categoria>()))
                .Throws(new Exception());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AdicionarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

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
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Categoria());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarInvalidoAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Categoria());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarCategoriaComando(Guid.NewGuid(), It.IsAny<String>(), 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarCategoriaNaoExistenteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDominioExcpetionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Categoria>(It.IsAny<CategoriaViewModel>()))
                .Returns(new Categoria());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Categoria());

            categoriaRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Categoria>()))
                .Throws(new DominioException());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<Categoria>(It.IsAny<CategoriaViewModel>()))
                .Returns(new Categoria());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Categoria());

            categoriaRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Categoria>()))
                .Throws(new Exception());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new AtualizarCategoriaComando(Guid.NewGuid(), "Descricao", 10, 10, 10);

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

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
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>()))
                .ReturnsAsync(new List<Categoria>() { new Categoria() });

            categoriaRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterProdutosPorCategoriaId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Produto>() { new Produto() });

            produtoRepositorioMock
                 .Setup(x => x.AtualizaProdutoCategoriaBase(It.IsAny<Guid>(), It.IsAny<Guid>()))
                 .Returns(Task.CompletedTask);

            var command = new RemoverCategoriaComando(Guid.NewGuid());

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComRemoverSemCategoriaBaseAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new RemoverCategoriaComando(Guid.NewGuid());

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComRemoverInvalidoBaseAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            var command = new RemoverCategoriaComando(It.IsAny<Guid>());

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarDomainExcpetionQuandoLidarComRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>()))
                .ReturnsAsync(new List<Categoria>() { new Categoria() });

            categoriaRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Throws(new DominioException());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterProdutosPorCategoriaId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Produto>() { new Produto() });

            var command = new RemoverCategoriaComando(Guid.NewGuid());

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExcpetionQuandoLidarComRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns(new CategoriaViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var categoriaRepositorioMock = new Mock<ICategoriaRepositorio>();

            categoriaRepositorioMock
                .Setup(x => x.Buscar(It.IsAny<Expression<Func<Categoria, bool>>>()))
                .ReturnsAsync(new List<Categoria>() { new Categoria() });

            categoriaRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .Throws(new Exception());

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();

            produtoRepositorioMock
                .Setup(x => x.ObterProdutosPorCategoriaId(It.IsAny<Guid>()))
                .ReturnsAsync(new List<Produto>() { new Produto() });

            var command = new RemoverCategoriaComando(Guid.NewGuid());

            var handler = new CategoriaCommandHandler(mediatorMock.Object, categoriaRepositorioMock.Object, mapperMock.Object, produtoRepositorioMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        #endregion
    }
}
