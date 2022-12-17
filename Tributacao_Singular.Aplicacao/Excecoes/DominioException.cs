using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Aplicacao.Excecoes
{
    [ExcludeFromCodeCoverage]
    public class DominioException : Exception
    {
        public DominioException()
        { }

        public DominioException(string message) : base(message)
        { }

        public DominioException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
