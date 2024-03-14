using FluentValidation;

namespace FuDever.Application.Features.Major.GetAllMajorsByMajorName;

/// <summary>
///     Get all majors by major name request validator.
/// </summary>
public sealed class GetAllMajorsByMajorNameRequestValidator : AbstractValidator<GetAllMajorsByMajorNameRequest>
{
    public GetAllMajorsByMajorNameRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.MajorName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: majorName =>
                !string.IsNullOrWhiteSpace(value: majorName))
            .Must(predicate: majorName => majorName.Length <=
                Domain.Entities.Major.Metadata.Name.MaxLength)
            .Must(predicate: majorName => majorName.Length >=
                Domain.Entities.Major.Metadata.Name.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
