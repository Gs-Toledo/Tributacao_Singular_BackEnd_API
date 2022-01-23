using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tributacao_Singular.Negocio.Interfaces
{
    public interface IUnidadeTrabalho
    {
        Task<int> CompletarAsync(CancellationToken cancellationToken);
    }
}
