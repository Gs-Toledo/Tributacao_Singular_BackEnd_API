using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos
{
    public class AtualizarProdutoComando : Comando
    {
        public Guid Id { get; set; }

        public string descricao { get; set; }

        public string NCM { get; set; }

        public string EAN { get; set; }

        public int Status { get; set; }

        public Guid CategoriaId { get; set; }

        public AtualizarProdutoComando(Guid Id, string descricao, string nCM, string eAN, int status, Guid categoriaId)
        {
            this.Id = Id;
            this.descricao = descricao;
            NCM = nCM;
            EAN = eAN;
            Status = status;
            CategoriaId = categoriaId;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AtualizarProdutoValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AtualizarProdutoValidacao : AbstractValidator<AtualizarProdutoComando>
    {
        public AtualizarProdutoValidacao()
        {

            RuleFor(c => c.descricao)
                .NotEmpty()
                .WithMessage("descricao não foi informado.");

            RuleFor(c => c.EAN)
                .NotEmpty()
                .WithMessage("CNPJ não foi informado.");

            RuleFor(c => c.NCM)
                .NotEmpty()
                .WithMessage("NCM não foi informado.");

            RuleFor(c => c.Status)
                .NotEmpty()
                .WithMessage("Status não foi informado.");
        }
    }
}
