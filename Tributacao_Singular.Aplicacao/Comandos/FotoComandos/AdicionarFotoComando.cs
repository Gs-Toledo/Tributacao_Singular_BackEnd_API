using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vir_Fundos_Infraestrutura.Mensagens;

namespace Tributacao_Singular.Aplicacao.Comandos.FotoComandos
{
    public class AdicionarFotoComando : Comando
    {
        public Guid Id { get; set; }
        public byte[] Src { get; set; }
        public Guid idUsuario { get; set; }

        public AdicionarFotoComando(Guid Id, byte[] _Src, Guid _idUsuario)
        {
            this.Id = Id;
            Src = _Src;
            idUsuario = _idUsuario;
        }
        public override bool EhValido()
        {
            ResultadoValidacao = new AdicionarFotoValidacao().Validate(this);
            return ResultadoValidacao.IsValid;
        }

    }
    public class AdicionarFotoValidacao : AbstractValidator<AdicionarFotoComando>
    {
        public AdicionarFotoValidacao()
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
