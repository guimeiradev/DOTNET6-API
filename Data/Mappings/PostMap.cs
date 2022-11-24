using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Mappings {
    public class PostMap : IEntityTypeConfiguration<Post> {
        public void Configure(EntityTypeBuilder<Post> builder) {
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.LastUpdateDate)
                .IsRequired()
                .HasColumnName("LastUpdateDate")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60)
                .HasDefaultValueSql("GETDATE()"); // PARA USAR O TIPO DO DATE TIME NOW DO BANCO
                //.HasDefaultValue(DateTime.Now.ToUniversalTime()); 

            // INDICE
            builder.HasIndex(x => x.Slug, "IX_POSTMAP_SLUG")
                .IsUnique(); // DIZER QUE É UNICO

            // RELACIONAMENTOS 

            // 1 PARA MUITOS
            builder.HasOne(x => x.Author) // RELACIONAMENTO 1 PARA 1. PODERIA SER TAMBEM OWNS ONE.
                .WithMany(x => x.Posts) // NESTE CASO ESTAMOS FAZENDO 1 PARA MUITOS
                .HasConstraintName("FK_POST_AUTHOR")
                .OnDelete(DeleteBehavior.Cascade); // ESSE CARA VAI QUANDO FOR FEITO UMA EXCLUSAO VAI EXCLUIR O AUTOR E OS POSTS DELE. ESTUDAR BEM NA HORA DE USAR ESSE CARA.

            // MUITOS PARA MUITOS
            builder.HasMany(x => x.Tags) // VAI TER MUITAS TAG
                 .WithMany(x => x.Posts) // OU MUITOS POSTS
                 .UsingEntity<Dictionary<string, object>>( // GERANDO UMA TABELA VIRTUAL, BASEADA EM DICIONARIO. QUE RECEBE DOIS VALORES UMA STRING E UM OBJETO
                 "PostTag", // NOSSA STRING É O NOME DA TABELA
                            // E O NOSSO OBJETO NESSE CASO ESTAMOS DIVIDINDO EM DOIS. O POST ELE TEM UMA TAG E ESSA TAG TEM MUITOS POSTS... A TAG TEM UM POST E ESSE POST TEM MUITAS TAGS.
                            // EMTAO, VAMOS TER UMA TABELA COM DOIS CAMPOS PostId e TagId E DUAS CONSTRAINTS FK_PostTag_PostId E FK_PosTag_TagId
                 post => post.HasOne<Tag>()
                 .WithMany()
                 .HasForeignKey("PostId")
                 .HasConstraintName("FK_PostTag_PostId")
                 .OnDelete(DeleteBehavior.Cascade),

                 tag => tag.HasOne<Post>()
                 .WithMany()
                 .HasForeignKey("TagId")
                 .HasConstraintName("FK_PosTag_TagId")
                 .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
