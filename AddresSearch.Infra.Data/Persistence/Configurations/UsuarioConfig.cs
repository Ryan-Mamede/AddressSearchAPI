using AddressSearch.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressSearch.Infra.Data.Persistence.Configurations
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> b)
        {
            b.ToTable("USUARIO");
            b.HasKey(x => x.IdUsuario); 

            b.Property(x => x.IdUsuario)
             .ValueGeneratedNever();                   
                                                       

            b.Property(x => x.Nome).HasMaxLength(150).IsRequired();
            b.Property(x => x.Email).HasMaxLength(100).IsRequired();
            b.HasIndex(x => x.Email).IsUnique();

            b.Property(x => x.Senha).HasMaxLength(255).IsRequired();
            b.Property(x => x.DataInclusao).HasColumnType("datetime2");
        }
    }
}
