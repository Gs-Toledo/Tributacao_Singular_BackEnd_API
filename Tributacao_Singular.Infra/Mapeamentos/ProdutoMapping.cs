using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tributacao_Singular.Negocio.Modelos;

namespace Tributacao_Singular.Infra.Mapeamentos
{
    [ExcludeFromCodeCoverage]
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.descricao)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(x => x.EAN)
                .IsRequired()
                .HasColumnType("varchar(13)");

            builder.Property(x => x.NCM)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.HasOne(x => x.Categoria)
                .WithMany(p => p.Produtos)
                .HasForeignKey(x => x.CategoriaId);

            builder.HasOne(x => x.Cliente)
                .WithMany(p => p.Produtos)
                .HasForeignKey(x => x.ClienteId);
        }
    }
}
