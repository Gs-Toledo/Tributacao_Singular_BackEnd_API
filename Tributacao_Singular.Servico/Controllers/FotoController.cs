using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tributacao_Singular.Aplicacao.Servicos;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Servico.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Foto")]
    public class FotoController : ApiControllerBase
    {
        private readonly IFotoServicoApp fotoServicoApp;

        public FotoController(IMediatorHandler mediadorHandler,
                              INotificationHandler<NotificacaoDominio> notificacoesHandler,
                              IUser user,
                              IFotoServicoApp fotoServicoApp) : base(notificacoesHandler, mediadorHandler, user)
        {
            this.fotoServicoApp = fotoServicoApp;
        }

        [HttpGet("Obter-Por-Id/{idUsuario:guid}")]
        public async Task<IActionResult> ObterPorIdUsuario(Guid idUsuario)
        {
            var foto = await fotoServicoApp.RecuperarFoto(idUsuario);

            return Response(foto);
        }


        [HttpPost("Adicionar")]
        public async Task<IActionResult> AdicionarFoto([FromForm] FotoArquivoViewModel fotoArquivoViewModel)
        {
            if (!ModelState.IsValid) return Response(ModelState);

            var fotoViewModel = new FotoViewModel()
            {
                Id = fotoArquivoViewModel.Id,
                idUsuario = fotoArquivoViewModel.idUsuario
            };

            if (fotoArquivoViewModel.Src.Length > 0)
            {
                using(var ms = new MemoryStream())
                {
                    fotoArquivoViewModel.Src.CopyTo(ms);
                    fotoViewModel.Src = ms.ToArray();
                }
            }

            await fotoServicoApp.AdicionarAsync(fotoViewModel);

            return Response();
        }


        [HttpPut("Atualizar/{id:guid}")]
        public async Task<IActionResult> AtualizarFoto(Guid id, FotoViewModel fotoViewModel)
        {
            if (id != fotoViewModel.Id)
            {
                NotifyError(string.Empty, "O id informado não é o mesmo que foi passado na query");
                return Response(fotoViewModel);
            }

            if (!ModelState.IsValid) return Response(ModelState);

            await fotoServicoApp.AtualizarAsync(fotoViewModel);

            return Response("Foto Atualizada com Sucesso!");
        }

        [HttpDelete("Remover/{id:guid}")]
        public async Task<IActionResult> RemoverFoto(Guid Id)
        {
            await fotoServicoApp.RemoverAsync(Id);

            return Response("Foto Removida com Sucesso!");
        }



    }
}
