using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tributacao_Singular.Negocio.Interfaces;
using Vir_Fundos_Infraestrutura.Comunicacao.Mediador;
using Vir_Fundos_Infraestrutura.Mensagens.Notificacao;

namespace Tributacao_Singular.Servico.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly NotificacaoDominioHandler _notifications;
        private readonly IMediatorHandler _mediator;
        public readonly IUser AppUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected ApiControllerBase(INotificationHandler<NotificacaoDominio> notifications,
                        IMediatorHandler mediator,
                        IUser appUser)
        {
            _notifications = (NotificacaoDominioHandler)notifications;
            _mediator = mediator;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected IEnumerable<NotificacaoDominio> Notifications => _notifications.ObterNotificacoes();

        protected bool OperacaoValida()
        {
            return (!_notifications.TemNotificacao());
        }

        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.ObterNotificacoes().Select(n => n.Valor)
            });
        }

        protected IActionResult ValidateModelState(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) 
            {
                var erros = modelState.Values.SelectMany(e => e.Errors);
                foreach (var erro in erros)
                {
                    var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                    NotifyError(string.Empty, errorMsg);
                }
            } 
  
            return Response();
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.PublicarNotificacao(new NotificacaoDominio(code, message));
        }
    }
}
