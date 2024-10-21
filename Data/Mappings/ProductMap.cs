using api_jet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_jet.Data.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Tabela
            builder.ToTable("Product");
            //Chave-Primary
            builder.HasKey(x => x.Id);

            //Identity
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.Property(x => x.Title)
                .IsRequired() // NOT NULL
                .HasColumnName("Title") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(150); //TAMANHO DO CAMPO

            

            builder.Property(x => x.Description)
                .HasColumnName("Description") //NOME DA COLUNA
                .HasColumnType("VARCHAR") //TIPO DE DADO DA COLUNA
                .HasMaxLength(2000); //TAMANHO DO CAMPO

            builder.Property(x => x.Stock)
                .IsRequired() // NOT NULL
                .HasColumnName("Stock") //NOME DA COLUNA
                .HasColumnType("INT"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.Status)
                .IsRequired() // NOT NULL
                .HasColumnName("Status") //NOME DA COLUNA
                .HasColumnType("boolean"); //TIPO DE DADO DA COLUNA

            builder.Property(x => x.Image)
                .HasColumnName("Image") //NOME DA COLUNA
                .HasColumnType("VARCHAR")
                .HasMaxLength(5000); 

            builder.Property(x => x.Price)
                .IsRequired() // NOT NULL
                .HasColumnName("Price") //NOME DA COLUNA
                .HasColumnType("FLOAT"); //TIPO DE DADO DA COLUNA

            //Indíces
            builder.HasIndex(x => x.Title, "IX_Product_Title")
                .IsUnique(); //Indice é único

        }
    }
}
