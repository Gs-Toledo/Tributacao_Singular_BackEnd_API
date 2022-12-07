using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Aplicacao.ViewModels;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Aplicacao.Servicos
{
    public interface IFotoServicoApp
    {
        Task<bool> AdicionarAsync(FotoViewModel foto);

        Task<bool> AtualizarAsync(FotoViewModel foto);

        Task<bool> RemoverAsync(Guid id);

        Task<FotoViewModel> RecuperarFoto(Guid idUsuario);
    }
}
