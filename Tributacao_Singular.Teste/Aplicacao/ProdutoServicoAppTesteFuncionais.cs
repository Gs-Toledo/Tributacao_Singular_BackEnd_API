using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.ClienteComandos;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Teste.Aplicacao
{
    public class ProdutoServicoAppTesteFuncionais : IClassFixture<Inicializar>
    {
        private readonly ServiceProvider provedorServico;

        public ProdutoServicoAppTesteFuncionais(Inicializar inicializar)
        {
            provedorServico = inicializar.ProvedorServico;
        }

        [Fact]
        public async Task ListarTodosTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();

            mapperMock
                .Setup(x => x.Map<List<ProdutoViewModel>>(It.IsAny<List<Produto>>()))
                .Returns(new List<ProdutoViewModel>
                {
                    new ProdutoViewModel()
                });

            var repositorioMock = new Mock<IProdutoRepositorio>();

            var categorias = new List<Produto>()
            {
                new Produto()
            };

            repositorioMock
                .Setup(x => x.ObterTodos())
                .ReturnsAsync(categorias);

            var mediadorHandler = new Mock<IMediatorHandler>();

            var servico = new ProdutoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioMock.Object);

            var lista = await servico.ListarTodosAsync();

            Assert.True(lista.Any());
        }

        [Fact]
        public async Task DeveAdicionarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IProdutoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns((Produto src) => new ProdutoViewModel()
                {
                    descricao = src.descricao,
                    EAN = src.EAN,
                    Id = src.Id,
                    CategoriaId = src.CategoriaId,
                    ClienteId = src.ClienteId,
                    NCM = src.NCM,
                    Status = src.Status
                });

            mapperMock
                .Setup(m => m.Map<AdicionarProdutoComando>(It.IsAny<ProdutoViewModel>()))
                .Returns((ProdutoViewModel src) => new AdicionarProdutoComando(src.Id, src.descricao, src.NCM, src.EAN, src.Status, src.ClienteId));

            repositorioCategoriaMock
                .Setup(c => c.Adicionar(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            ProdutoServicoApp servico = new ProdutoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var adicionou = await servico.AdicionarAsync(mapperMock.Object.Map<ProdutoViewModel>(new Produto("Descricao","NCM","EAN",new Categoria(), new Cliente(), 0)));

            Assert.True(adicionou);
        }

        [Fact]
        public async Task DeveAtualizarTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IProdutoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns((Produto src) => new ProdutoViewModel()
                {
                    descricao = src.descricao,
                    EAN = src.EAN,
                    Id = src.Id,
                    CategoriaId = src.CategoriaId,
                    ClienteId = src.ClienteId,
                    NCM = src.NCM,
                    Status = src.Status
                });

            mapperMock
                .Setup(m => m.Map<AdicionarProdutoComando>(It.IsAny<ProdutoViewModel>()))
                .Returns((ProdutoViewModel src) => new AdicionarProdutoComando(src.Id, src.descricao, src.NCM, src.EAN, src.Status, src.ClienteId));

            repositorioCategoriaMock
                .Setup(c => c.Atualizar(It.IsAny<Produto>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            ProdutoServicoApp servico = new ProdutoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var atualizou = await servico.AtualizarAsync(mapperMock.Object.Map<ProdutoViewModel>(new Produto("Descricao", "NCM", "EAN", new Categoria(), new Cliente(), 0)));

            Assert.True(atualizou);
        }

        [Fact]
        public async Task DeveRemoverTesteAsync()
        {
            var mapperMock = new Mock<IMapper>();
            var repositorioCategoriaMock = new Mock<IProdutoRepositorio>();
            var mediadorHandler = new Mock<IMediatorHandler>();

            mapperMock
                .Setup(m => m.Map<ProdutoViewModel>(It.IsAny<Produto>()))
                .Returns((Produto src) => new ProdutoViewModel()
                {
                    descricao = src.descricao,
                    EAN = src.EAN,
                    Id = src.Id,
                    CategoriaId = src.CategoriaId,
                    ClienteId = src.ClienteId,
                    NCM = src.NCM,
                    Status = src.Status
                });

            mapperMock
                .Setup(m => m.Map<AdicionarProdutoComando>(It.IsAny<ProdutoViewModel>()))
                .Returns((ProdutoViewModel src) => new AdicionarProdutoComando(src.Id, src.descricao, src.NCM, src.EAN, src.Status, src.ClienteId));

            repositorioCategoriaMock
                .Setup(c => c.Remover(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);

            mediadorHandler
                .Setup(m => m.EnviarComando(It.IsAny<Comando>()))
                .ReturnsAsync(() => true);

            ProdutoServicoApp servico = new ProdutoServicoApp(mapperMock.Object, mediadorHandler.Object, repositorioCategoriaMock.Object);

            var removeu = await servico.RemoverAsync(It.IsAny<Guid>());

            Assert.True(removeu);
        }
    }
}
