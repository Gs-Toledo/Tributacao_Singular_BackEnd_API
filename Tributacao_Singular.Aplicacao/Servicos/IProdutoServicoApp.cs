using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.ViewModels;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public interface IProdutoServicoApp
    {

        Task<bool> AtualizarAsync(ProdutoViewModel produtoViewModel);

        Task<bool> RemoverAsync(Guid id);

        Task<List<ProdutoViewModel>> ListarTodosAsync();

        Task<ProdutoViewModel> ObterPorIdAsync(Guid id);
    }
}
