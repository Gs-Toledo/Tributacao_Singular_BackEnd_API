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
    public class ClienteRepositorio : Repository<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(MeuDbContext db) : base(db) { }

        public async Task<Cliente> ObterPorCnpj(string cnpj)
        {
            return await Db.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.cnpj == cnpj);

        }

        public async Task<Cliente> ObterClienteProdutosPorId(Guid Id)
        {
            return await Db.Clientes.AsNoTracking()
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Id == Id);

        }
    }
}
