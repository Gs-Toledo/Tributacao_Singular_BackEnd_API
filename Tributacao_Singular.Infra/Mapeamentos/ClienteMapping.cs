using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Infra.Mapeamentos
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.nome)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(x => x.cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.ToTable("Clientes");
        }
    }
}
