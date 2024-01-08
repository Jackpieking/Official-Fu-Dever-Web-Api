using Domain.Specifications.Entities.Blog.Manager;
using Domain.Specifications.Entities.Cv.Manager;
using Domain.Specifications.Entities.Department.Manager;
using Domain.Specifications.Entities.Hobby.Manager;
using Domain.Specifications.Entities.Major.Manager;
using Domain.Specifications.Entities.Platform.Manager;
using Domain.Specifications.Entities.Position.Manager;
using Domain.Specifications.Entities.Project.Manager;
using Domain.Specifications.Entities.RefreshToken.Manager;
using Domain.Specifications.Entities.Role.Manager;
using Domain.Specifications.Entities.Skill.Manager;
using Domain.Specifications.Entities.User.Manager;
using Domain.Specifications.Entities.UserHobby.Manager;
using Domain.Specifications.Entities.UserJoiningStatus.Manager;
using Domain.Specifications.Entities.UserPlatform.Manager;
using Domain.Specifications.Entities.UserSkill.Manager;

namespace Domain.Specifications.Others.Interfaces;

/// <summary>
///     Represent a factory contain all entity
///     specification manager.
/// </summary>
public interface ISuperSpecificationManager
{
    /// <summary>
    ///     Blog specification manager.
    /// </summary>
    IBlogSpecificationManager Blog { get; }

    /// <summary>
    ///     Skill specification manager.
    /// </summary>
    ISkillSpecificationManager Skill { get; }

    /// <summary>
    ///     Position specification manager.
    /// </summary>
    IPositionSpecificationManager Position { get; }

    /// <summary>
    ///     Platform specification manager.
    /// </summary>
    IPlatformSpecificationManager Platform { get; }

    /// <summary>
    ///     Major specification manager.
    /// </summary>
    IMajorSpecificationManager Major { get; }

    /// <summary>
    ///     Hobby specification manager.
    /// </summary>
    IHobbySpecificationManager Hobby { get; }

    /// <summary>
    ///     Department specification manager.
    /// </summary>
    IDepartmentSpecificationManager Department { get; }

    /// <summary>
    ///     Project specification manager.
    /// </summary>
    IProjectSpecificationManager Project { get; }

    /// <summary>
    ///     Role specification manager.
    /// </summary>
    IRoleSpecificationManager Role { get; }

    /// <summary>
    ///     User specification manager.
    /// </summary>
    IUserSpecificationManager User { get; }

    /// <summary>
    ///     User joining status specification manager.
    /// </summary>
    IUserJoiningStatusSpecificationManager UserJoiningStatus { get; }

    /// <summary>
    ///     User platform specification manager.
    /// </summary>
    IUserPlatformSpecificationManager UserPlatform { get; }

    /// <summary>
    ///     Cv specification manager.
    /// </summary>
    ICvSpecificationManager Cv { get; }

    /// <summary>
    ///     User skill specification manager.
    /// </summary>
    IUserSkillSpecificationManager UserSkill { get; }

    /// <summary>
    ///     User hobby specification manager.
    /// </summary>
    IUserHobbySpecificationManager UserHobby { get; }

    /// <summary>
    ///     Refresh token specification manager.
    /// </summary>
    IRefreshTokenSpecificationManager RefreshToken { get; }
}
