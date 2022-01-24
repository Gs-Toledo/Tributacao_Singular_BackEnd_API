using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.ProdutoComandos
{
    public class AdicionarProdutoComando : Comando
    {
        public Guid Id { get; set; }

        public string descricao { get; set; }

        public string NCM { get; set; }

        public string EAN { get; set; }

        public int Status { get; set; }

        public Guid ClienteId { get; set; }

        public AdicionarProdutoComando(Guid id, string descricao, string nCM, string eAN, int status, Guid clienteId)
        {
            Id = id;
            this.descricao = descricao;
            NCM = nCM;
            EAN = eAN;
            Status = status;
            ClienteId = clienteId;
        }

        public override bool EhValido()
        {
            ResultadoValidacao = new AdicionarProdutoValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }

    public class AdicionarProdutoValidacao : AbstractValidator<AdicionarProdutoComando>
    {
        public AdicionarProdutoValidacao()
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

            RuleFor(c => c.ClienteId)
                .NotEmpty()
                .WithMessage("Id Cliente não foi informado.");
        }
    }
}
