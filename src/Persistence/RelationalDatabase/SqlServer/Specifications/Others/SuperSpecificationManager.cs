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
using Domain.Specifications.Others.Interfaces;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Blog.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Cv.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Department.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Hobby.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Major.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Platform.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Position.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Project.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.RefreshToken.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Role.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Skill.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.User.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserHobby.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserJoiningStatus.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserPlatform.Manager;
using Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserSkill.Manager;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Others;

internal sealed class SuperSpecificationManager : ISuperSpecificationManager
{
    // Backing fields
    private ISkillSpecificationManager _skillSpecificationManager;
    private IPositionSpecificationManager _positionSpecificationManager;
    private IPlatformSpecificationManager _platformSpecificationManager;
    private IMajorSpecificationManager _majorSpecificationManager;
    private IHobbySpecificationManager _hobbySpecificationManager;
    private IDepartmentSpecificationManager _departmentSpecificationManager;
    private IProjectSpecificationManager _projectSpecificationManager;
    private IRoleSpecificationManager _roleSpecificationManager;
    private IUserSpecificationManager _userSpecificationManager;
    private IUserJoiningStatusSpecificationManager _userJoiningStatusSpecificationManager;
    private IUserPlatformSpecificationManager _userPlatformSpecificationManager;
    private IBlogSpecificationManager _blogSpecificationManager;
    private ICvSpecificationManager _cvSpecificationManager;
    private IUserSkillSpecificationManager _userSkillSpecificationManager;
    private IUserHobbySpecificationManager _userHobbySpecificationManager;
    private IRefreshTokenSpecificationManager _refreshTokenSpecificationManager;

    public ISkillSpecificationManager Skill
    {
        get
        {
            _skillSpecificationManager ??= new SkillSpecificationManager();

            return _skillSpecificationManager;
        }
    }

    public IPositionSpecificationManager Position
    {
        get
        {
            _positionSpecificationManager ??= new PositionSpecificationManager();

            return _positionSpecificationManager;
        }
    }

    public IPlatformSpecificationManager Platform
    {
        get
        {
            _platformSpecificationManager ??= new PlatformSpecificationManager();

            return _platformSpecificationManager;
        }
    }

    public IMajorSpecificationManager Major
    {
        get
        {
            _majorSpecificationManager ??= new MajorSpecificationManager();

            return _majorSpecificationManager;
        }
    }

    public IHobbySpecificationManager Hobby
    {
        get
        {
            _hobbySpecificationManager ??= new HobbySpecificationManager();

            return _hobbySpecificationManager;
        }
    }

    public IDepartmentSpecificationManager Department
    {
        get
        {
            _departmentSpecificationManager ??= new DepartmentSpecificationManager();

            return _departmentSpecificationManager;
        }
    }

    public IProjectSpecificationManager Project
    {
        get
        {
            _projectSpecificationManager ??= new ProjectSpecificationManager();

            return _projectSpecificationManager;
        }
    }

    public IRoleSpecificationManager Role
    {
        get
        {
            _roleSpecificationManager ??= new RoleSpecificationManager();

            return _roleSpecificationManager;
        }
    }

    public IUserSpecificationManager User
    {
        get
        {
            _userSpecificationManager ??= new UserSpecificationManager();

            return _userSpecificationManager;
        }
    }

    public IUserJoiningStatusSpecificationManager UserJoiningStatus
    {
        get
        {
            _userJoiningStatusSpecificationManager ??= new UserJoiningStatusSpecificationManager();

            return _userJoiningStatusSpecificationManager;
        }
    }

    public IUserPlatformSpecificationManager UserPlatform
    {
        get
        {
            _userPlatformSpecificationManager ??= new UserPlatformSpecificationManager();

            return _userPlatformSpecificationManager;
        }
    }

    public IBlogSpecificationManager Blog
    {
        get
        {
            _blogSpecificationManager ??= new BlogSpecificationManager();

            return _blogSpecificationManager;
        }
    }

    public ICvSpecificationManager Cv
    {
        get
        {
            _cvSpecificationManager ??= new CvSpecificationManager();

            return _cvSpecificationManager;
        }
    }

    public IUserSkillSpecificationManager UserSkill
    {
        get
        {
            _userSkillSpecificationManager ??= new UserSkillSpecificationManager();

            return _userSkillSpecificationManager;
        }
    }

    public IUserHobbySpecificationManager UserHobby
    {
        get
        {
            _userHobbySpecificationManager ??= new UserHobbySpecificationManager();

            return _userHobbySpecificationManager;
        }
    }

    public IRefreshTokenSpecificationManager RefreshToken
    {
        get
        {
            _refreshTokenSpecificationManager ??= new RefreshTokenSpecificationManager();

            return _refreshTokenSpecificationManager;
        }
    }
}