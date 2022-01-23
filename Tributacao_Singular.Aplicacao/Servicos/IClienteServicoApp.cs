using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.ViewModels;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public interface IClienteServicoApp
    {
        Task<bool> AdicionarAsync(ClienteViewModel clienteViewModel);

        Task<bool> AtualizarAsync(ClienteViewModel clienteViewModel);

        Task<bool> RemoverAsync(Guid id);

        Task<bool> AdicionarProdutosAsync(ClienteViewModel clienteViewModel);

        Task<List<ClienteViewModel>> ListarTodosAsync();

        Task<ClienteViewModel> ObterPorIdAsync(Guid id);
    }
}
