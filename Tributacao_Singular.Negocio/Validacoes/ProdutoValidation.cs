using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Negocio.Validacoes
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(p => p.descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.EAN)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(13)
                .WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");

            RuleFor(p => p.NCM)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8)
                .WithMessage("O campo {PropertyName} precisa ter {MinLength} caracteres");
        }
    }
}
