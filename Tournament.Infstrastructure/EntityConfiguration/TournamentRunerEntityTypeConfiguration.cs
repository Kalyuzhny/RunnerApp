using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Tournament.Domain.AgregatesModel;

namespace Tournament.Infstrastructure.EntityConfiguration
{
    public class TournamentRunnerEntityTypeConfiguration : IEntityTypeConfiguration<TournamentRunnerModel>
    {
        public void Configure(EntityTypeBuilder<TournamentRunnerModel> builder)
        {
            builder.ToTable("tournament_runner", TournamentDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TournamentId)
                .HasColumnName("TournamentId")
                .IsRequired();

            builder.Property(t => t.RunnerId)
                .HasColumnName("RunnerId")
                .IsRequired();

            builder.HasOne<TournamentModel>()
                .WithMany()
                .IsRequired(true)
                .HasForeignKey("RunnerId");
        }
    }
}
