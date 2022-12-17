using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Modelos
{
    public class Categoria : Entity
    {
        public string descricao { get; set; }

        public decimal ICMS { get; set; }

        public decimal Cofins { get; set; }

        public decimal IPI { get; set; }

        public IEnumerable<Produto> Produtos {get; set;}

        public Categoria()
        {
            this.descricao = String.Empty;
            this.Produtos = new List<Produto>();
        }

        public Categoria(string descricao, decimal iCMS, decimal cofins, decimal iPI, List<Produto> produtos)
        {
            this.descricao = descricao;
            this.ICMS = iCMS;
            this.Cofins = cofins;
            this.IPI = iPI;
            this.Produtos = produtos;
        }

        public bool EhValido()
        {
            var resultadoValidacao = new CategoriasValidation().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }

    public class CategoriasValidation : AbstractValidator<Categoria>
    {
        public CategoriasValidation()
        {
            RuleFor(p => p.descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(p => p.Cofins)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.ICMS)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.IPI)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
