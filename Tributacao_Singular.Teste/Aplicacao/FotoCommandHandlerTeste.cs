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
using Tributacao_Singular.Aplicacao.Comandos.FotoComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;
using Tributacao_Singular.Aplicacao.Excecoes;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class FotoCommandHandlerTeste : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;


        public FotoCommandHandlerTeste(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }
        #region comandos
        [Fact]
        public void DeveGerarUmAdicionarComando()
        {
            AdicionarFotoComando adicionarFotoComando = new AdicionarFotoComando(Guid.NewGuid(),new byte[10],Guid.NewGuid());

            Assert.True(adicionarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAdicionarComandoErroId()
        {
            AdicionarFotoComando adicionarFotoComando = new AdicionarFotoComando(It.IsAny<Guid>(), new byte[10], Guid.NewGuid());

            Assert.False(adicionarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAdicionarComandoErroSrc()
        {
            AdicionarFotoComando adicionarFotoComando = new AdicionarFotoComando(Guid.NewGuid(), It.IsAny<byte[]>(), Guid.NewGuid());

            Assert.False(adicionarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAdicionarComandoErroIdUsuario()
        {
            AdicionarFotoComando adicionarFotoComando = new AdicionarFotoComando(Guid.NewGuid(), new byte[10], It.IsAny<Guid>());

            Assert.False(adicionarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAtualizarComando() 
        {
            AtualizarFotoComando atualizarFotoComando = new AtualizarFotoComando(Guid.NewGuid(), new byte[10], Guid.NewGuid());

            Assert.True(atualizarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAtualizarComandoErroId()
        {
            AtualizarFotoComando atualizarFotoComando = new AtualizarFotoComando(It.IsAny<Guid>(), new byte[10], Guid.NewGuid());

            Assert.False(atualizarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAtualizarComandoErroSrc()
        {
            AtualizarFotoComando atualizarFotoComando = new AtualizarFotoComando(Guid.NewGuid(), It.IsAny<byte[]>(), Guid.NewGuid());

            Assert.False(atualizarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmAtualizarComandoErroIdUsuario()
        {
            AtualizarFotoComando atualizarFotoComando = new AtualizarFotoComando(Guid.NewGuid(), new byte[10], It.IsAny<Guid>());

            Assert.False(atualizarFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmRemoverComando()
        {
            RemoverFotoComando removerFotoComando = new RemoverFotoComando(Guid.NewGuid());

            Assert.True(removerFotoComando.EhValido());
        }

        [Fact]
        public void DeveGerarUmRemoverComandoErroId()
        {
            RemoverFotoComando removerFotoComando = new RemoverFotoComando(It.IsAny<Guid>());

            Assert.False(removerFotoComando.EhValido());
        }
        #endregion

        #region AdicionarHandler

        [Fact]
        public async Task DeveLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            var command = new AdicionarFotoComando(Guid.NewGuid(), new byte[0], Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveGerarExeptionQuandoLidarComAdicionarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            fotoRepositorioMock
                .Setup(x => x.Adicionar(It.IsAny<Foto>()))
                .ThrowsAsync(new Exception());

            var command = new AdicionarFotoComando(Guid.NewGuid(), new byte[0], Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

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
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            fotoRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Foto(new byte[0], Guid.NewGuid()));

            var command = new AtualizarFotoComando(Guid.NewGuid(), new byte[0], Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveLidarComAtualizarFotoInexistenteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            var command = new AtualizarFotoComando(Guid.NewGuid(), new byte[0], Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        [Fact]
        public async Task DeveGerarExeptionQuandoLidarComAtualizarAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            fotoRepositorioMock
                .Setup(x => x.ObterPorId(It.IsAny<Guid>()))
                .ReturnsAsync(new Foto(new byte[0], Guid.NewGuid()));

            fotoRepositorioMock
                .Setup(x => x.Atualizar(It.IsAny<Foto>()))
                .ThrowsAsync(new Exception());

            var command = new AtualizarFotoComando(Guid.NewGuid(), new byte[0], Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

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
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            var command = new RemoverFotoComando(Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

            var result = await handler.Handle(command, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeveGerarExeptionQuandoLidarComRemoverAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns(new FotoViewModel());

            var mediatorMock = new Mock<IMediatorHandler>();

            var fotoRepositorioMock = new Mock<IFotoRepositorio>();

            fotoRepositorioMock
                .Setup(x => x.Remover(It.IsAny<Guid>()))
                .ThrowsAsync(new DominioException());

            var command = new RemoverFotoComando(Guid.NewGuid());

            var handler = new FotoCommandHandler(mediatorMock.Object, fotoRepositorioMock.Object, mapperMock.Object);

            var result = await handler.Handle(command, default);

            mediatorMock.Verify(x => x.PublicarNotificacao(It.IsAny<NotificacaoDominio>()));
            Assert.False(result);
        }

        #endregion
    }
}
