using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class CategoriaServicoAppTesteFuncionais : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public CategoriaServicoAppTesteFuncionais(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        [Fact]
        public async Task ListarTodosTesteAsync() 
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<List<CategoriaViewModel>>(It.IsAny<List<Categoria>>()))
                .Returns(new List<CategoriaViewModel> 
                {
                    new CategoriaViewModel()
                });

            var repositorioMock = new Mock<ICategoriaRepositorio>();

            var categorias = new List<Categoria>()
            {
                new Categoria()
            };

            repositorioMock
                .Setup(x => x.ObterTodos())
                .ReturnsAsync(categorias);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new CategoriaServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ListarTodosAsync();

            Assert.True(lista.Any());
        }

        [Fact]
        public async Task DeveAdicionarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<ICategoriaRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns((Categoria src) => new CategoriaViewModel()
                {
                    Cofins = src.Cofins,
                    descricao = src.descricao,
                    ICMS = src.ICMS,
                    Id = src.Id,
                    IPI = src.IPI
                });

            mapperMock
                .Setup(m => m.Map<AdicionarCategoriaComando>(It.IsAny<CategoriaViewModel>()))
                .Returns((CategoriaViewModel src) => new AdicionarCategoriaComando(src.Id, src.descricao, src.ICMS, src.Cofins, src.IPI));

            repositorioCategoriaMock
                .Setup(c => c.Adicionar(It.IsAny<Categoria>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            CategoriaServicoApp servico = new CategoriaServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.AdicionarAsync(mapperMock.Object.Map<CategoriaViewModel>(new Categoria("Descricao", 10, 10, 10, It.IsAny<List<Produto>>())));

            Assert.True(adicionou);
        }

        [Fact]
        public async Task DeveAtualizarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<ICategoriaRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns((Categoria src) => new CategoriaViewModel()
                {
                    Cofins = src.Cofins,
                    descricao = src.descricao,
                    ICMS = src.ICMS,
                    Id = src.Id,
                    IPI = src.IPI
                });

            mapperMock
                .Setup(m => m.Map<AtualizarCategoriaComando>(It.IsAny<CategoriaViewModel>()))
                .Returns((CategoriaViewModel src) => new AtualizarCategoriaComando(src.Id, src.descricao, src.ICMS, src.Cofins, src.IPI));

            repositorioCategoriaMock
                .Setup(c => c.Atualizar(It.IsAny<Categoria>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            var servico = new CategoriaServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var atualizou = await servico.AtualizarAsync(mapperMock.Object.Map<CategoriaViewModel>(new Categoria("Descricao", 10, 10, 10, It.IsAny<List<Produto>>())));

            Assert.True(atualizou);
        }

        [Fact]
        public async Task DeveRemoverTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<ICategoriaRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<CategoriaViewModel>(It.IsAny<Categoria>()))
                .Returns((Categoria src) => new CategoriaViewModel()
                {
                    Cofins = src.Cofins,
                    descricao = src.descricao,
                    ICMS = src.ICMS,
                    Id = src.Id,
                    IPI = src.IPI
                });

            mapperMock
                .Setup(m => m.Map<RemoverCategoriaComando>(It.IsAny<CategoriaViewModel>()))
                .Returns((CategoriaViewModel src) => new RemoverCategoriaComando(src.Id));

            repositorioCategoriaMock
                .Setup(c => c.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            var servico = new CategoriaServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var removeu = await servico.RemoverAsync(It.IsAny<Guid>());

            Assert.True(removeu);
        }
    }
}
