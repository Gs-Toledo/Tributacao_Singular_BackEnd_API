using FluentValidation.Results;
using MediatR;
using System;

namespace Vir_Fundos_Infraestrutura.Mensagens
{
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
