using FuDever.Domain.Entities;
using FuDever.PostgresSql.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FuDever.PostgresSql.Data.EntityConfigurations;

/// <summary>
///     Represent "Positions" table configuration.
/// </summary>
internal sealed class PositionEntityConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        const string TableName = "Positions";
        const string TableComment = "Contain position record.";

        builder.ToTable(
            name: TableName,
            buildAction: table => table.HasComment(comment: TableComment));

        // Id property configuration.
        builder.HasKey(keyExpression: position => position.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: position => position.Name)
            .HasColumnType(typeName: CommonConstant.DbDataType.VarcharGenerator.Get(
                length: Position
                    .Metadata
                    .Name
                    .MaxLength))
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: position => position.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.TIMESTAMPTZ)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: position => position.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Positions] - [Users] (1 - N).
        builder
            .HasMany(navigationExpression: position => position.Users)
            .WithOne(navigationExpression: user => user.Position)
            .HasForeignKey(foreignKeyExpression: user => user.PositionId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}