using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos
{
    public class AtualizarCategoriaComando : Comando
    {
        public Guid Id { get; set; }

        public string descricao { get; set; }

        public decimal ICMS { get; set; }

        public decimal Cofins { get; set; }

        public decimal IPI { get; set; }

        public AtualizarCategoriaComando(Guid id, string descricao, decimal iCMS, decimal cofins, decimal iPI)
        {
            Id = id;
            this.descricao = descricao;
            ICMS = iCMS;
            Cofins = cofins;
            IPI = iPI;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AtualizarCategoriaValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AtualizarCategoriaValidacao : AbstractValidator<AtualizarCategoriaComando>
    {
        public AtualizarCategoriaValidacao()
        {

            RuleFor(c => c.descricao)
                .NotEmpty()
                .WithMessage("Nome não foi informado.");

            RuleFor(c => c.ICMS)
                .NotEmpty()
                .WithMessage("ICMS não foi informado.");

            RuleFor(c => c.IPI)
                .NotEmpty()
                .WithMessage("IPI não foi informado.");

            RuleFor(c => c.Cofins)
                .NotEmpty()
                .WithMessage("Cofins não foi informado.");
        }
    }
}
