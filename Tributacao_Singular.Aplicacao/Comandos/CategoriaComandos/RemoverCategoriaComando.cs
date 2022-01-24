using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos
{
    public class RemoverCategoriaComando : Comando
    {
        public Guid Id { get; set; }

        public RemoverCategoriaComando(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new RemoverCategoriaValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class RemoverCategoriaValidacao : AbstractValidator<RemoverCategoriaComando>
    {
        public RemoverCategoriaValidacao()
        {

            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Nome não foi informado.");
        }
    }
}
