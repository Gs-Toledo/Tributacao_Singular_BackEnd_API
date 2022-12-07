using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Negocio.Interfaces
{
    public interface IFotoRepositorio : IRepositorio<Foto>
    {
        Task<Foto> RecuperarFotoUsuario(Guid idUsuario);
    }
}

