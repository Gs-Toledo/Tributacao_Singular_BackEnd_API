using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.CategoriaComandos
{
    public class AdicionarCategoriaComando : Comando
    {
        public Guid Id { get; set; }

        public string descricao { get; set; }

        public decimal ICMS { get; set; }

        public decimal Cofins { get; set; }

        public decimal IPI { get; set; }

        public AdicionarCategoriaComando(Guid Id, string descricao, decimal iCMS, decimal cofins, decimal iPI)
        {
            this.Id = Id;
            this.descricao = descricao;
            ICMS = iCMS;
            Cofins = cofins;
            IPI = iPI;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AdicionarCategoriaValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AdicionarCategoriaValidacao : AbstractValidator<AdicionarCategoriaComando>
    {
        public AdicionarCategoriaValidacao()
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
