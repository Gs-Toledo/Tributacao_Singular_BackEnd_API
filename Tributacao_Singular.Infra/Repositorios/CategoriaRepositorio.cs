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
    public class CategoriaRepositorio : Repository<Categoria>, ICategoriaRepositorio 
    {
        public CategoriaRepositorio(MeuDbContext db) : base(db) { }

        public async Task<Categoria> ObterCategoriaProdutosPorId(Guid Id)
        {
            return await Db.Categorias.AsNoTracking()
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Id == Id);

        }
    }
}
