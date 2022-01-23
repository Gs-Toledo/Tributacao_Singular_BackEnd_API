using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public class ProdutoServicoApp : IProdutoServicoApp
    {
        private readonly IMapper mapper;
        private readonly IMediatorHandler mediador;
        private readonly IProdutoRepositorio respositorioProduto;

        public ProdutoServicoApp(IMapper mapper, IMediatorHandler mediador, IProdutoRepositorio respositorioProduto)
        {
            this.mapper = mapper;
            this.mediador = mediador;
            this.respositorioProduto = respositorioProduto;
        }

        public async Task<bool> AtualizarAsync(ProdutoViewModel produtoViewModel)
        {
            var adicionarComando = mapper.Map<AtualizarProdutoComando>(produtoViewModel);
            return await mediador.EnviarComando(adicionarComando);
        }

        public async Task<List<ProdutoViewModel>> ListarTodosAsync()
        {
            return mapper.Map<List<ProdutoViewModel>>(await respositorioProduto.ObterTodos());
        }

        public async Task<ProdutoViewModel> ObterPorIdAsync(Guid id)
        {
            return mapper.Map<ProdutoViewModel>(await respositorioProduto.ObterPorId(id));
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var produtoModel = new ProdutoViewModel();
            produtoModel.Id = id;

            var adicionarComando = mapper.Map<RemoverProdutoComando>(produtoModel);
            return await mediador.EnviarComando(adicionarComando);
        }
    }
}
