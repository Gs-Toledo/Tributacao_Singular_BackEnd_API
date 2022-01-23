using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Negocio.Interfaces
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        Task<Cliente> ObterPorCnpj(string cnpj);

        Task<Cliente> ObterClienteProdutosPorId(Guid Id);
    }
}
