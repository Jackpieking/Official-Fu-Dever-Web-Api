using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Commons;

namespace Persistence.Database.SqlServer.Data.EntityConfigurations;

/// <summary>
///     Represent "Departments" tables configuration.
/// </summary>
internal sealed class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        const string TableName = "Departments";
        const string TableComment = "Contain department record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: department => department.Id);

        // Name property configuration.
        builder
            .Property(propertyExpression: department => department.Name)
            .HasColumnType(typeName: CommonConstant.DbDataType.NvarcharGenerator.Get(
                length: Department.Metadata.Name.MaxLength))
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: department => department.RemovedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME_2)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: department => department.RemovedBy)
            .IsRequired();

        // Table relationships configurations.
        // [Departments] - [Users] (1 - N).
        builder
            .HasMany(navigationExpression: department => department.Users)
            .WithOne(navigationExpression: user => user.Department)
            .HasForeignKey(foreignKeyExpression: user => user.DepartmentId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}