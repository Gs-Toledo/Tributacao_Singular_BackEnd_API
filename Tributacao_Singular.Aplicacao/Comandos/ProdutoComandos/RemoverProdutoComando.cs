using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos
{
    public class RemoverProdutoComando : Comando
    {
        public Guid Id { get; set; }

        public RemoverProdutoComando(Guid id)
        {
            Id = id;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new RemoverProdutoValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class RemoverProdutoValidacao : AbstractValidator<RemoverProdutoComando>
    {
        public RemoverProdutoValidacao()
        {

            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
