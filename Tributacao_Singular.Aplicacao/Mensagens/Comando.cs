using FluentValidation.Results;
using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Vir_Fundos_Infraestrutura.Mensagens
{
    [ExcludeFromCodeCoverage]
    public abstract class Comando : Mensagem, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ResultadoValidacao { get; set; }

        protected Comando()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
