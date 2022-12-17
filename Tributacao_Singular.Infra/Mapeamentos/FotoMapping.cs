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
    public class FotoMapping
    {
        public void Configure(EntityTypeBuilder<Foto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Src)
                .IsRequired().HasMaxLength(8000);

            builder.ToTable("Fotos");
        }
    }
}
