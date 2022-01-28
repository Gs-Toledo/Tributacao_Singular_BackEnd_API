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
    [Route("api/Categoria")]
    public class CategoriaController : ApiControllerBase
    {
        private readonly ICategoriaServicoApp categoriaServicoApp;

        public CategoriaController(IMediatorHandler mediadorHandler,
                              INotificationHandler<NotificacaoDominio> notificacoesHandler,
                              IUser user,
                              ICategoriaServicoApp categoriaServicoApp) : base(notificacoesHandler, mediadorHandler, user)
        {
            this.categoriaServicoApp = categoriaServicoApp;
        }

        [ClaimsAuthorize("Tributarista,Administrador", "Listar")]
        [HttpGet("Obter-Todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var listaClientes = await categoriaServicoApp.ListarTodosAsync();

            return Response(listaClientes);
        }

        [ClaimsAuthorize("Tributarista,Administrador", "Listar")]
        [HttpGet("Obter-Por-Id/{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid Id)
        {
            var cliente = await categoriaServicoApp.ObterPorIdAsync(Id);

            return Response(cliente);
        }

        [ClaimsAuthorize("Tributarista,Administrador", "Adicionar")]
        [HttpPost("Adicionar")]
        public async Task<IActionResult> ObterPorId(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            await categoriaServicoApp.AdicionarAsync(categoriaViewModel);

            return Response();
        }

        [ClaimsAuthorize("Tributarista", "Atualizar")]
        [HttpPut("Atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarCliente(Guid id, CategoriaViewModel categoriaViewModel)
        {
            if (id != categoriaViewModel.Id)
            {
                NotifyError(string.Empty, "O id informado não é o mesmo que foi passado na query");
                return Response(categoriaViewModel);
            }

            if (!ModelState.IsValid) return Response(ModelState);

            await categoriaServicoApp.AtualizarAsync(categoriaViewModel);

            return Response("Cliente Atualizado com Sucesso!");
        }

        [ClaimsAuthorize("Tributarista", "Remover")]
        [HttpDelete("Remover/{id:guid}")]
        public async Task<IActionResult> RemoverCliente(Guid Id)
        {
            await categoriaServicoApp.RemoverAsync(Id);

            return Response("Cliente Removido com Sucesso!");
        }
    }
}
