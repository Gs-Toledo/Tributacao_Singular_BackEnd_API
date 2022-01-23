using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.ClienteComandos
{
    public class RemoverClienteComando : Comando
    {
        public Guid Id { get; set; }

        public RemoverClienteComando(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new RemoverClienteValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class RemoverClienteValidacao : AbstractValidator<RemoverClienteComando>
    {
        public RemoverClienteValidacao()
        {

            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
