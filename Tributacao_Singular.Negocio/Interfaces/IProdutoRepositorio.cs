using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Negocio.Interfaces
{
    public interface IProdutoRepositorio : IRepositorio<Produto>
    {
        Task<List<Produto>> ObterProdutosPorClienteId(Guid id);

        Task<List<Produto>> ObterProdutosPorCategoriaId(Guid id);
    }
}
