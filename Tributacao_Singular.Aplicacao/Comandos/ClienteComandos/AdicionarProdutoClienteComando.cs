using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.ViewModels;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.ClienteComandos
{
    public class AdicionarProdutoClienteComando : Comando
    {
        public Guid Id { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

        public AdicionarProdutoClienteComando(Guid id, IEnumerable<ProdutoViewModel> produtos)
        {
            Id = id;
            Produtos = produtos;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AdicionarProdutoClienteValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AdicionarProdutoClienteValidacao : AbstractValidator<AdicionarProdutoClienteComando>
    {
        public AdicionarProdutoClienteValidacao()
        {

            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id não foi informado.");

            RuleFor(c => c.Produtos)
                .NotEmpty()
                .WithMessage("Produtos não foi informado.");
        }
    }
}
