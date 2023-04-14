using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Domain.AgregatesModel;

namespace Tournament.Infstrastructure.EntityConfiguration
{
    public class TournamentEntityTypeConfiguration : IEntityTypeConfiguration<TournamentModel>
    {
        public void Configure(EntityTypeBuilder<TournamentModel> builder)
        {
            builder.ToTable("tournaments", TournamentDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(t => t.Title)
                .HasColumnName("Title")
                .IsRequired();

            builder.Property(t => t.Country)
                .HasColumnName("Country")
                .IsRequired();

            builder.HasMany(t => t.Runners)
                .WithOne()
                .IsRequired(true)
                .HasForeignKey("RunnerId");
        }
    }
}
