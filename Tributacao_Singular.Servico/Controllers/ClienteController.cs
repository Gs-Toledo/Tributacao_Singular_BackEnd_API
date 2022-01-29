using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public ClienteController(IMediatorHandler mediadorHandler,
                              INotificationHandler<NotificacaoDominio> notificacoesHandler,
                              IUser user,
                              UserManager<IdentityUser> _userManager,
                              IClienteServicoApp clienteServicoApp) : base(notificacoesHandler, mediadorHandler, user)
        {
            this.clienteServicoApp = clienteServicoApp;
            this._userManager = _userManager;
        }

        [ClaimsAuthorize("Cliente,Administrador", "Listar")]
        [HttpGet("Obter-Todos")]
        public async Task<IActionResult> ObterTodos() 
        {
            var listaClientes = await clienteServicoApp.ObterTodosClienteProdutosAsync();

            return Response(listaClientes);
        }

        [ClaimsAuthorize("Cliente,Administrador", "Listar")]
        [HttpGet("Obter-Por-Id/{id:Guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var cliente = await clienteServicoApp.ObterClienteProdutosPorIdAsync(id);

            return Response(cliente);
        }

        [ClaimsAuthorize("Cliente,Administrador", "Atualizar")]
        [HttpPut("Atualizar/{id:Guid}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, ClienteViewModel clienteViewModel)
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

        [ClaimsAuthorize("Administrador", "Remover")]
        [HttpDelete("Remover/{id:Guid}")]
        public async Task<IActionResult> RemoverCliente(Guid id)
        {
            try 
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    await clienteServicoApp.RemoverAsync(id);

                    return Response("Cliente Removido com Sucesso!");
                }
                else
                {
                    return Response("Erro na remoção do Cliente: " + id);
                }
            }
            catch(Exception e) 
            {
                return Response(e.Message);
            }
        }

    }
}
