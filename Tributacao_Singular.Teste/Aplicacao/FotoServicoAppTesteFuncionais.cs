using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.FotoComandos;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class FotoServicoAppTesteFuncionais : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public FotoServicoAppTesteFuncionais(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        [Fact]
        public async Task DeveAdicionarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IFotoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns((Foto src) => new FotoViewModel()
                {
                    Id = Guid.NewGuid(),
                    idUsuario = Guid.NewGuid(),
                    Src = new byte[0]
                });

            mapperMock
                .Setup(m => m.Map<AdicionarFotoComando>(It.IsAny<FotoViewModel>()))
                .Returns((FotoViewModel src) => new AdicionarFotoComando(src.Id, src.Src, src.idUsuario));

            repositorioCategoriaMock
                .Setup(c => c.Adicionar(It.IsAny<Foto>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            FotoServicoApp servico = new FotoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.AdicionarAsync(mapperMock.Object.Map<FotoViewModel>(new Foto(new byte[0],Guid.NewGuid())));

            Assert.True(adicionou);
        }

        [Fact]
        public async Task DeveAtualizarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IFotoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns((Foto src) => new FotoViewModel()
                {
                    Id = Guid.NewGuid(),
                    idUsuario = Guid.NewGuid(),
                    Src = new byte[0]
                });

            mapperMock
                .Setup(m => m.Map<AdicionarFotoComando>(It.IsAny<FotoViewModel>()))
                .Returns((FotoViewModel src) => new AdicionarFotoComando(src.Id, src.Src, src.idUsuario));

            repositorioCategoriaMock
                .Setup(c => c.Atualizar(It.IsAny<Foto>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            FotoServicoApp servico = new FotoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.AtualizarAsync(mapperMock.Object.Map<FotoViewModel>(new Foto(new byte[0], Guid.NewGuid())));

            Assert.True(adicionou);
        }

        [Fact]
        public async Task DeveRemoverTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IFotoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns((Foto src) => new FotoViewModel()
                {
                    Id = Guid.NewGuid(),
                    idUsuario = Guid.NewGuid(),
                    Src = new byte[0]
                });

            mapperMock
                .Setup(m => m.Map<AdicionarFotoComando>(It.IsAny<FotoViewModel>()))
                .Returns((FotoViewModel src) => new AdicionarFotoComando(src.Id, src.Src, src.idUsuario));

            repositorioCategoriaMock
                .Setup(c => c.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            FotoServicoApp servico = new FotoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.RemoverAsync(It.IsAny<Guid>());

            Assert.True(adicionou);
        }

        [Fact]
        public async Task ObterPorIdAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IFotoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<FotoViewModel>(It.IsAny<Foto>()))
                .Returns((Foto src) => new FotoViewModel()
                {
                    Id = Guid.NewGuid(),
                    idUsuario = Guid.NewGuid(),
                    Src = new byte[0]
                });

            mapperMock
                .Setup(m => m.Map<AdicionarFotoComando>(It.IsAny<FotoViewModel>()))
                .Returns((FotoViewModel src) => new AdicionarFotoComando(src.Id, src.Src, src.idUsuario));

            var foto = new Foto();

            repositorioCategoriaMock
                .Setup(c => c.RecuperarFotoUsuario(It.IsAny<Guid>()))
                .ReturnsAsync(foto);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            FotoServicoApp servico = new FotoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.RecuperarFoto(It.IsAny<Guid>());

            Assert.True(adicionou != null);
        }
    }
}
