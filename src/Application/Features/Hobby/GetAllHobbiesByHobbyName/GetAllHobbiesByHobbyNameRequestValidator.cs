using FluentValidation;

namespace Application.Features.Hobby.GetAllHobbiesByHobbyName;

/// <summary>
///     Request validator for getting all hobbies by hobby name.
/// </summary>
public sealed class GetAllHobbiesByHobbyNameRequestValidator : AbstractValidator<GetAllHobbiesByHobbyNameRequest>
{
    public GetAllHobbiesByHobbyNameRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.HobbyName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: hobbyName =>
                !string.IsNullOrWhiteSpace(value: hobbyName))
            .Must(predicate: hobbyName => hobbyName.Length <=
                Domain.Entities.Hobby.Metadata.Name.MaxLength)
            .Must(predicate: hobbyName => hobbyName.Length >=
                Domain.Entities.Hobby.Metadata.Name.MinLength);

        RuleFor(expression: request => request.CacheExpiredTime)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .Must(predicate: cacheExpiredTime => cacheExpiredTime >= default(int));
    }
}
