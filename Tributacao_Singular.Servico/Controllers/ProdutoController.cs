using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Servico.Extensoes;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Servico.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Produto")]
    public class ProdutoController : ApiControllerBase
    {
        private readonly IProdutoServicoApp produtoServicoApp;

        public ProdutoController(IMediatorHandler mediadorHandler,
                              INotificationHandler<NotificacaoDominio> notificacoesHandler,
                              IUser user,
                              IProdutoServicoApp produtoServicoApp) : base(notificacoesHandler, mediadorHandler, user)
        {
            this.produtoServicoApp = produtoServicoApp;
        }

        [ClaimsAuthorize("Cliente,Administrador", "Listar")]
        [HttpGet("Obter-Todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var listaClientes = await produtoServicoApp.ListarTodosAsync();

            return Response(listaClientes);
        }

        [ClaimsAuthorize("Cliente,Administrador", "Listar")]
        [HttpGet("Obter-Por-Id/{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid Id)
        {
            var cliente = await produtoServicoApp.ObterPorIdAsync(Id);

            return Response(cliente);
        }

        [ClaimsAuthorize("Cliente", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarProduto(ListaProdutoViewModel produtoViewModels)
        {
            if (!ModelState.IsValid) return ValidateModelState(ModelState);

            foreach (var item in produtoViewModels.produtoViewModels) 
            {
                await produtoServicoApp.AdicionarAsync(item);
            }

            return Response("Produtos Adicionados com Sucesso!");
        }

        [ClaimsAuthorize("Cliente,Tributarista", "Atualizar")]
        [HttpPut("Atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id)
            {
                NotifyError(string.Empty, "O id informado não é o mesmo que foi passado na query");
                return Response(produtoViewModel);
            }

            if (!ModelState.IsValid) return Response(ModelState);

            await produtoServicoApp.AtualizarAsync(produtoViewModel);

            return Response("Produto Atualizado com Sucesso!");
        }

        [ClaimsAuthorize("Cliente", "Remover")]
        [HttpDelete("Remover/{id:guid}")]
        public async Task<IActionResult> RemoverProduto(Guid Id)
        {
            await produtoServicoApp.RemoverAsync(Id);

            return Response("Produto Removido com Sucesso!");
        }
    }
}
