using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;
using System;

namespace FuDever.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of update
///     field of hobby specification.
/// </summary>
internal sealed class UpdateFieldOfHobbySpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    IUpdateFieldOfHobbySpecification
{
    public IUpdateFieldOfHobbySpecification Ver1(
        DateTime hobbyRemovedAt,
        Guid hobbyRemovedBy)
    {
        // Validate hobby removed time.
        if (hobbyRemovedAt == DateTime.MaxValue)
        {
            return default;
        }

        // Validate hobby remover.
        if (hobbyRemovedBy == Guid.Empty)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall
            .SetProperty(
                hobby => hobby.RemovedAt,
                hobbyRemovedAt)
            .SetProperty(
                hobby => hobby.RemovedBy,
                hobbyRemovedBy);

        return this;
    }

    public IUpdateFieldOfHobbySpecification Ver2(string hobbyName)
    {
        // Validate hobby name.
        if (string.IsNullOrWhiteSpace(value: hobbyName) ||
            hobbyName.Length > Domain.Entities.Hobby.Metadata.Name.MaxLength ||
            hobbyName.Length < Domain.Entities.Hobby.Metadata.Name.MinLength)
        {
            return default;
        }

        UpdateExpression = setPropertyCall => setPropertyCall.SetProperty(
            hobby => hobby.Name,
            hobbyName);

        return this;
    }
}
