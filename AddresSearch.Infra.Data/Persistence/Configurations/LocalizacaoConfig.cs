using AddressSearch.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressSearch.Infra.Data.Persistence.Configurations;

public class LocalizacaoConfig : IEntityTypeConfiguration<Localizacao>
{
    public void Configure(EntityTypeBuilder<Localizacao> b)
    {
        b.ToTable("Localizacao");
        b.HasKey(x => x.Id);

        b.Property(x => x.Cep).HasMaxLength(8).IsRequired();
        b.HasIndex(x => x.Cep).IsUnique();

        b.Property(x => x.Logradouro).HasMaxLength(200).IsRequired();
        b.Property(x => x.Bairro).HasMaxLength(150).IsRequired();
        b.Property(x => x.LocalidadeNome).HasMaxLength(150).IsRequired();
        b.Property(x => x.Uf).HasMaxLength(2).IsRequired();

        b.Property(x => x.Complemento).HasMaxLength(200);
        b.Property(x => x.Ibge).HasMaxLength(20);
        b.Property(x => x.Gia).HasMaxLength(20);
        b.Property(x => x.Ddd).HasMaxLength(5);
        b.Property(x => x.Siafi).HasMaxLength(20);
    }
}
