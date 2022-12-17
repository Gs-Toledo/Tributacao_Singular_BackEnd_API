using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Modelos
{
    public class Cliente : Entity
    {
        public string nome { get; set; }

        public string cnpj { get; set; }

        /* EF Relation */
        public IEnumerable<Produto> Produtos { get; set; }

        public Cliente()
        {
            this.nome = "";
            this.cnpj = "";
            this.Produtos = new List<Produto>();
        }

        public Cliente(string nome, List<Produto> produtos, string cnpj)
        {
            this.nome = nome;
            this.cnpj = cnpj;
            this.Produtos = produtos;
        }

        public bool EhValido()
        {
            var resultadoValidacao = new ClienteValidation().Validate(this);
            return resultadoValidacao.IsValid;
        }
    }

    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(p => p.nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 150)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
