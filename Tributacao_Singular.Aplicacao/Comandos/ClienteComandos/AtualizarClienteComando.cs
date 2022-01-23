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
    public class AtualizarClienteComando : Comando
    {
        public Guid Id { get; set; }

        public string nome { get; set; }

        public string cnpj { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

        public AtualizarClienteComando(Guid id, string cnpj, IEnumerable<ProdutoViewModel> produtos, string nome)
        {
            Id = id;
            this.cnpj = cnpj;
            Produtos = produtos;
            this.nome = nome;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AtualizarClienteValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AtualizarClienteValidacao : AbstractValidator<AtualizarClienteComando>
    {
        public AtualizarClienteValidacao()
        {

            RuleFor(c => c.nome)
                .NotEmpty()
                .WithMessage("Nome não foi informado.");

            RuleFor(c => c.cnpj)
                .NotEmpty()
                .WithMessage("CNPJ não foi informado.");
        }
    }
}
