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
    public class LogMapping : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.usuario)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder
                .Property(x => x.Tabela)
                .IsRequired()
                .HasColumnType("varchar(150)");
        }
    }
}
