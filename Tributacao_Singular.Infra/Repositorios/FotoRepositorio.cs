using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Infra.Contexto;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Infra.Repositorios
{
    public class FotoRepositorio : Repository<Foto>, IFotoRepositorio
    {
        public FotoRepositorio(MeuDbContext db) : base(db)
        {
        }

        public async Task<Foto> RecuperarFotoUsuario(Guid idUsuario)
        {
            return await Db.Fotos.AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.idUsuario == idUsuario);
        }
    }
}
