using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.FotoComandos
{
    public class AtualizarFotoComando : Comando
    {
        public Guid Id { get; set; }

        public byte[] Src { get; set; }

        public Guid idUsuario { get; set; }

        public AtualizarFotoComando(Guid id, byte[] Src, Guid idUsuario)
        {
            Id = id;
            this.Src = Src;
            this.idUsuario = idUsuario;
        }
        public override bool EhValido()
        {
            ResultadoValidacao = new AtualizarFotoValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }
    }
    public class AtualizarFotoValidacao : AbstractValidator<AtualizarFotoComando>
    {
        public AtualizarFotoValidacao()
        {

            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id não informado.");

            RuleFor(c => c.Src)
                .NotEmpty()
                .WithMessage("Src não informado.");

            RuleFor(c => c.idUsuario)
                .NotEmpty()
                .WithMessage("idUsuario não informado.");
        }
    }
}
