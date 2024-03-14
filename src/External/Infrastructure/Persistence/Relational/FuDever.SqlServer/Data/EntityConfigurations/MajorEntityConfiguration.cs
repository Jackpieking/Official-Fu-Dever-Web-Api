using FuDever.Domain.Entities;
using FuDever.SqlServer.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "Majors" table configuration.
/// </summary>
internal sealed class MajorEntityConfiguration : IEntityTypeConfiguration<Major>
{
    public void Configure(EntityTypeBuilder<Major> builder)
    {
        const string TableName = "Majors";
        const string TableComment = "Contain major record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));

        // Primary key configuration.
        builder.HasKey(keyExpression: major => major.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: major => major.Name)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: Major
                    .Metadata
                    .Name
                    .MaxLength))
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: major => major.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: major => major.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Majors] - [Users] (1 - N).
        builder
            .HasMany(navigationExpression: major => major.Users)
            .WithOne(navigationExpression: user => user.Major)
            .HasForeignKey(foreignKeyExpression: user => user.MajorId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}