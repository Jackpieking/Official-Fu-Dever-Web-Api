using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data.EntityConfigurations;

/// <summary>
///     Represent "RefreshTokens" table configuration.
/// </summary>
internal sealed class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        const string TableName = "RefreshTokens";
        const string TableComment = "Contain refresh token record.";

        builder.ToTable(
            name: TableName,
            buildAction: table =>
            {
                table.HasComment(comment: TableComment);
            });

        // Primary key configuration.
        builder.HasKey(keyExpression: refreshToken => refreshToken.Id);

        // UserId property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.UserId)
            .IsRequired();

        // Value property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.Value)
            .HasColumnType(typeName: CommonConstant.DbDataType.NVARCHAR_MAX)
            .IsRequired();

        // AccessTokenId property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.AccessTokenId)
            .IsRequired();

        // ExpiredDate property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.ExpiredDate)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.CreatedAt)
            .HasColumnType(typeName: CommonConstant.DbDataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: refreshToken => refreshToken.CreatedBy)
            .IsRequired();
    }
}