using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Runner.Domain.AgregatesModel;

namespace Runner.Infstrastructure.EntityConfiguration
{
    public class RunnerEntityTypeConfiguration : IEntityTypeConfiguration<RunnerModel>
    {
        public void Configure(EntityTypeBuilder<RunnerModel> builder)
        {
            builder.ToTable("runners", RunnerDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .IsRequired();

            builder.Property(t => t.Surname)
                .HasColumnName("Surname")
                .IsRequired();

            builder.Property(t => t.Age)
                .HasColumnName("Age")
                .IsRequired();

            builder.Property(t => t.Rank)
                .HasColumnName("Rank")
                .IsRequired();
        }
    }
}
