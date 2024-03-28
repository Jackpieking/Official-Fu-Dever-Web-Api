using System;
using FuDever.Domain.EntityBuilders.RefreshToken.Others;

namespace FuDever.Domain.EntityBuilders.RefreshToken;

public sealed class RefreshTokenForNewRecordBuilder :
    Entities.RefreshToken,
    IBaseRefreshTokenBuilder,
    IRefreshTokenBuilder<RefreshTokenForNewRecordBuilder>
{
    public void Clear()
    {
        Id = Guid.Empty;
        Value = default;
        AccessTokenId = Guid.Empty;
        ExpiredDate = default;
        CreatedAt = default;
        CreatedBy = Guid.Empty;
    }

    public Entities.RefreshToken Complete()
    {
        return new()
        {
            Id = Id,
            Value = Value,
            AccessTokenId = AccessTokenId,
            ExpiredDate = ExpiredDate,
            CreatedAt = CreatedAt,
            CreatedBy = CreatedBy
        };
    }

    public RefreshTokenForNewRecordBuilder WithAccessTokenId(Guid accessTokenId)
    {
        // Validate access token id.
        if (accessTokenId == Guid.Empty)
        {
            return default;
        }

        AccessTokenId = accessTokenId;

        return this;
    }

    public RefreshTokenForNewRecordBuilder WithCreatedAt(DateTime refreshTokenCreatedAt)
    {
        // Validate refresh token created at.
        if (refreshTokenCreatedAt == DateTime.MaxValue ||
            refreshTokenCreatedAt == DateTime.MinValue ||
            refreshTokenCreatedAt.Kind != DateTimeKind.Utc)
        {
            return default;
        }

        CreatedAt = refreshTokenCreatedAt;

        return this;
    }

    public RefreshTokenForNewRecordBuilder WithCreatedBy(Guid refreshTokenCreatedBy)
    {
        // Validate refresh token creator.
        if (refreshTokenCreatedBy == Guid.Empty)
        {
            return default;
        }

        CreatedBy = refreshTokenCreatedBy;

        return this;
    }

    public RefreshTokenForNewRecordBuilder WithExpiredDate(DateTime refreshTokenExpiredDate)
    {
        // Validate refresh token expired date.
        if (refreshTokenExpiredDate == DateTime.MaxValue ||
            refreshTokenExpiredDate == DateTime.MinValue ||
            refreshTokenExpiredDate.Kind != DateTimeKind.Utc)
        {
            return default;
        }

        ExpiredDate = refreshTokenExpiredDate;

        return this;
    }

    public RefreshTokenForNewRecordBuilder WithId(Guid refreshTokenId)
    {
        // Validate refresh token id.
        if (refreshTokenId == Guid.Empty)
        {
            return default;
        }

        Id = refreshTokenId;

        return this;
    }

    public RefreshTokenForNewRecordBuilder WithValue(string refreshTokenValue)
    {
        // Validate refresh token value.
        if (string.IsNullOrWhiteSpace(value: refreshTokenValue) ||
            refreshTokenValue.Length > Metadata.Value.MaxLength ||
            refreshTokenValue.Length < Metadata.Value.MinLength)
        {
            return default;
        }

        Value = refreshTokenValue;

        return this;
    }
}
