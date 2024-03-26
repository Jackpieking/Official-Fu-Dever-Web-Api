using FuDever.WebApi.EntityHttpResponse.Entities.Department.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Position.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.Others;

namespace FuDever.WebApi.EntityHttpResponse.Others;

/// <summary>
///     Manage all entity http response manager.
/// </summary>
public sealed class EntityHttpResponseManager
{
    // Backing fields.
    private DepartmentHttpResponseManager _departmentHttpResponseManager;
    private HobbyHttpResponseManager _hobbyHttpResponseManager;
    private MajorHttpResponseManager _majorHttpResponseManager;
    private PlatformHttpResponseManager _platformHttpResponseManager;
    private PositionHttpResponseManager _positionHttpResponseManager;
    private RoleHttpResponseManager _roleHttpResponseManager;
    private SkillHttpResponseManager _skillHttpResponseManager;

    internal DepartmentHttpResponseManager Department
    {
        get
        {
            _departmentHttpResponseManager ??= new();

            return _departmentHttpResponseManager;
        }
    }

    internal HobbyHttpResponseManager Hobby
    {
        get
        {
            _hobbyHttpResponseManager ??= new();

            return _hobbyHttpResponseManager;
        }
    }

    internal MajorHttpResponseManager Major
    {
        get
        {
            _majorHttpResponseManager ??= new();

            return _majorHttpResponseManager;
        }
    }

    internal PlatformHttpResponseManager Platform
    {
        get
        {
            _platformHttpResponseManager ??= new();

            return _platformHttpResponseManager;
        }
    }

    internal PositionHttpResponseManager Position
    {
        get
        {
            _positionHttpResponseManager ??= new();

            return _positionHttpResponseManager;
        }
    }

    internal RoleHttpResponseManager Role
    {
        get
        {
            _roleHttpResponseManager ??= new();

            return _roleHttpResponseManager;
        }
    }

    internal SkillHttpResponseManager Skill
    {
        get
        {
            _skillHttpResponseManager ??= new();

            return _skillHttpResponseManager;
        }
    }
}
