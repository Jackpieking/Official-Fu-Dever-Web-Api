using FuDever.WebApi.AppCodes.Base;

namespace FuDever.WebApi.ApiReturnCodes;

/// <summary>
///     Represent skill entity api return code.
/// </summary>
internal abstract class SkillAppCode : BaseAppCode
{

    internal const int SKILL_IS_NOT_FOUND = 3;

    internal const int SKILL_ALREADY_EXISTS = 4;

    internal const int SKILL_IS_ALREADY_TEMPORARILY_REMOVED = 5;

    internal const int SKILL_IS_NOT_TEMPORARILY_REMOVED = 6;
}