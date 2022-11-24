using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings {
    public class CategoryMap : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {
            // TABELA
            builder.ToTable("Category");
            // CHAVE PRIMARIA
            builder.HasKey(x => x.Id);
            // IDENTITY
            builder.Property(x => x.Id) // PROPERTY USADO PARA QUALQUER PROPRIEDADE
                .ValueGeneratedOnAdd()
                .UseIdentityColumn(); // SERIA NO BANCO O IDENTITY(1,1)

            // PROPRIEDADES
            builder.Property(x => x.Name)
                .IsRequired() // DIZER QUE O CAMPO É REQUIRIDO. QUE SERIA O NOT NULL.
                .HasColumnName("Name") // DIZER O NOME DA COLUNA. CASO SEJA DIFERENTE DO NOME DA PROPRIEDADE.
                .HasColumnType("NVARCHAR") // TIPO DA COLUNA. E NO FLUNT NAO PRECISA COLOCAR A QUANTIDADE DE CARACTERES.
                .HasMaxLength(80); // AQUI QUE DEFINIMOS A QUANTIDADE MAXIMA DE CARACTERES.


            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // INDICES
            builder.HasIndex(x => x.Slug, "IX_CATEGORY_SLUG")
                .IsUnique(); // DIZER QUE É UNICO
        }
    }
}
