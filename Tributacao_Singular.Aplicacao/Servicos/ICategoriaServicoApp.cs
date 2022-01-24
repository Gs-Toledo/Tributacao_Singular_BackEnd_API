using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.ViewModels;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public interface ICategoriaServicoApp
    {
        Task<bool> AdicionarAsync(CategoriaViewModel categoriaViewModel);

        Task<bool> AtualizarAsync(CategoriaViewModel categoriaViewModel);

        Task<bool> RemoverAsync(Guid id);

        Task<List<CategoriaViewModel>> ListarTodosAsync();

        Task<CategoriaViewModel> ObterPorIdAsync(Guid id);
    }
}
