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
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.descricao)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(x => x.Cofins)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.ICMS)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.IPI)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        }
    }
}
