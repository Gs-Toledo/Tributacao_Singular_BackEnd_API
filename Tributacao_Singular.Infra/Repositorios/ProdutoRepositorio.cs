using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Infra.Contexto;
using Tributacao_Singular.Negocio.Interfaces;
using Tributacao_Singular.Negocio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Tributacao_Singular.Infra.Repositorios
{
    public class ProdutoRepositorio : Repository<Produto>, IProdutoRepositorio
    {
        private readonly MeuDbContext db;

        public ProdutoRepositorio(MeuDbContext db) : base(db) 
        {
            this.db = db;
        }

        public async Task<List<Produto>> ObterProdutosPorClienteId(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Where(x => x.ClienteId == id)
                .ToListAsync();
        }

        public async Task<List<Produto>> ObterProdutosPorCategoriaId(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Where(x => x.CategoriaId == id)
                .ToListAsync();
        }

        public async Task AtualizaProdutoCategoriaBase(Guid categoriaBaseId, Guid produtoId) 
        {
            db.Database.ExecuteSqlRaw($"UPDATE dbo.Produtos SET CategoriaId = '{categoriaBaseId}' WHERE Id = '{produtoId}' ");
            await db.CompletarAsync();
        }
    }
}
