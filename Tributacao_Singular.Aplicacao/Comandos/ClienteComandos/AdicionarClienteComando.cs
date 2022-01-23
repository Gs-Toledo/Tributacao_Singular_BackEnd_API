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
    public class AdicionarClienteComando : Comando
    {
        public Guid Id { get; set; }

        public string nome { get; set; }

        public string cnpj { get; set; }

        public IEnumerable<ProdutoViewModel> Produtos { get; set; }

        public AdicionarClienteComando(Guid id, string cnpj, IEnumerable<ProdutoViewModel> produtos, string nome)
        {
            Id = id;
            this.cnpj = cnpj;
            Produtos = produtos;
            this.nome = nome; 
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AdicionarClienteValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AdicionarClienteValidacao : AbstractValidator<AdicionarClienteComando>
    {
        public AdicionarClienteValidacao()
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
