using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.FotoComandos
{
    public class RemoverFotoComando : Comando
    {
        public Guid Id { get; set; }

        public RemoverFotoComando(Guid id)
        {
            Id = id;
        }
        public override bool EhValido()
        {
            ResultadoValidacao = new RemoverFotoValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }
    public class RemoverFotoValidacao : AbstractValidator<RemoverFotoComando>
    {
        public RemoverFotoValidacao()
        {

            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id não informado.");
        }
    }
}
