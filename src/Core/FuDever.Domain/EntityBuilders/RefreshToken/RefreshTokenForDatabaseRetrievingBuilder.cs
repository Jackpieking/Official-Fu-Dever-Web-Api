using FuDever.Domain.EntityBuilders.RefreshToken.Others;
using System;

namespace FuDever.Domain.EntityBuilders.RefreshToken;

/// <summary>
///     Refresh token for database retrieving builder.
/// </summary>
public sealed class RefreshTokenForDatabaseRetrievingBuilder :
    Entities.RefreshToken,
    IBaseRefreshTokenBuilder,
    IRefreshTokenBuilder<RefreshTokenForDatabaseRetrievingBuilder>
{
    public Entities.RefreshToken Complete()
    {
        return new()
        {
            Id = Id,
            Value = Value,
            AccessTokenId = AccessTokenId,
            ExpiredDate = ExpiredDate,
            CreatedAt = CreatedAt,
        };
    }

    public RefreshTokenForDatabaseRetrievingBuilder WithId(Guid refreshTokenId)
    {
        Id = refreshTokenId;

        return this;
    }

    public RefreshTokenForDatabaseRetrievingBuilder WithValue(string refreshTokenValue)
    {
        Value = refreshTokenValue;

        return this;
    }

    public RefreshTokenForDatabaseRetrievingBuilder WithAccessTokenId(Guid accessTokenId)
    {
        AccessTokenId = accessTokenId;

        return this;
    }

    public RefreshTokenForDatabaseRetrievingBuilder WithExpiredDate(DateTime refreshTokenExpiredDate)
    {
        ExpiredDate = refreshTokenExpiredDate;

        return this;
    }

    public RefreshTokenForDatabaseRetrievingBuilder WithCreatedAt(DateTime refreshTokenCreatedAt)
    {
        CreatedAt = refreshTokenCreatedAt;

        return this;
    }

    public RefreshTokenForDatabaseRetrievingBuilder WithCreatedBy(Guid refreshTokenCreatedBy)
    {
        CreatedBy = refreshTokenCreatedBy;

        return this;
    }

    public void Clear()
    {
        Id = Guid.Empty;
        Value = default;
        AccessTokenId = Guid.Empty;
        ExpiredDate = default;
        CreatedAt = default;
    }
}
