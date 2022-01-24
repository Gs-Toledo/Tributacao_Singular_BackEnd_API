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
    [Route("api/Cliente")]
    public class ClienteController : ApiControllerBase
    {
        private readonly IClienteServicoApp clienteServicoApp;

        public ClienteController(IMediatorHandler mediadorHandler,
                              INotificationHandler<NotificacaoDominio> notificacoesHandler,
                              IUser user,
                              IClienteServicoApp clienteServicoApp) : base(notificacoesHandler, mediadorHandler, user)
        {
            this.clienteServicoApp = clienteServicoApp;
        }

        [ClaimsAuthorize("Cliente,Administrador", "Listar")]
        [HttpGet("Obter-Todos")]
        public async Task<IActionResult> ObterTodos() 
        {
            var listaClientes = await clienteServicoApp.ObterTodosClienteProdutosAsync();

            return Response(listaClientes);
        }

        [ClaimsAuthorize("Cliente,Administrador", "Listar")]
        [HttpGet("Obter-Por-Id/{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var cliente = await clienteServicoApp.ObterClienteProdutosPorIdAsync(id);

            return Response(cliente);
        }

        [ClaimsAuthorize("Cliente", "Atualizar")]
        [HttpPut("Atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, [FromBody] ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id)
            {
                NotifyError(string.Empty, "O id informado não é o mesmo que foi passado na query");
                return Response(clienteViewModel);
            }

            if (!ModelState.IsValid) return Response(ModelState);

            await clienteServicoApp.AtualizarAsync(clienteViewModel);

            return Response("Cliente Atualizado com Sucesso!");
        }

        [ClaimsAuthorize("Cliente", "Remover")]
        [HttpDelete("Remover/{id:guid}")]
        public async Task<IActionResult> RemoverCliente(Guid Id)
        {
            await clienteServicoApp.RemoverAsync(Id);

            return Response("Cliente Removido com Sucesso!");
        }

    }
}
