using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.SqlServer2016.Data;
using Persistence.SqlServer2016.Repositories;

namespace Persistence.SqlServer2016.UnitOfWorks;

/// <summary>
///     Implementation of unit of work.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    //Backing fields.
    private readonly FuDeverContext _context;
    private IDbContextTransaction _dbContextTransaction;
    private IUserRepository _userRepository;
    private IRefreshTokenRepository _refreshTokenRepository;
    private IUserJoiningStatusRepository _userJoiningStatusRepository;
    private IRoleRepository _roleRepository;
    private ISkillRepository _skillRepository;
    private IHobbyRepository _hobbyRepository;
    private IUserHobbyRepository _userHobbyRepository;
    private IUserSkillRepository _userSkillRepository;
    private IUserPlatformRepository _userPlatformRepository;
    private IDepartmentRepository _departmentRepository;
    private IMajorRepository _majorRepository;
    private IPlatformRepository _platformRepository;
    private IPositionRepository _positionRepository;
    private IBlogCommentRepository _blogCommentRepository;
    private IBlogRepository _blogRepository;
    private IProjectRepository _projectRepository;
    private ICvRepository _cvRepository;

    public UnitOfWork(FuDeverContext context)
    {
        _context = context;
    }


    public IUserRepository UserRepository
    {
        get
        {
            _userRepository ??= new UserRepository(context: _context);

            return _userRepository;
        }
    }

    public IRefreshTokenRepository RefreshTokenRepository
    {
        get
        {
            _refreshTokenRepository ??= new RefreshTokenRepository(context: _context);

            return _refreshTokenRepository;
        }
    }

    public IUserJoiningStatusRepository UserJoiningStatusRepository
    {
        get
        {
            _userJoiningStatusRepository ??= new UserJoiningStatusRepository(context: _context);

            return _userJoiningStatusRepository;
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
            _roleRepository ??= new RoleRepository(context: _context);

            return _roleRepository;
        }
    }

    public IHobbyRepository HobbyRepository
    {
        get
        {
            _hobbyRepository ??= new HobbyRepository(context: _context);

            return _hobbyRepository;
        }
    }

    public ISkillRepository SkillRepository
    {
        get
        {
            _skillRepository ??= new SkillRepository(context: _context);

            return _skillRepository;
        }
    }

    public IUserHobbyRepository UserHobbyRepository
    {
        get
        {
            _userHobbyRepository ??= new UserHobbyRepository(context: _context);

            return _userHobbyRepository;
        }
    }

    public IUserSkillRepository UserSkillRepository
    {
        get
        {
            _userSkillRepository ??= new UserSkillRepository(context: _context);

            return _userSkillRepository;
        }
    }

    public IUserPlatformRepository UserPlatformRepository
    {
        get
        {
            _userPlatformRepository ??= new UserPlatformRepository(context: _context);

            return _userPlatformRepository;
        }
    }

    public IDepartmentRepository DepartmentRepository
    {
        get
        {
            _departmentRepository ??= new DepartmentRepository(context: _context);

            return _departmentRepository;
        }
    }

    public IPositionRepository PositionRepository
    {
        get
        {
            _positionRepository ??= new PositionRepository(context: _context);

            return _positionRepository;
        }
    }

    public IMajorRepository MajorRepository
    {
        get
        {
            _majorRepository ??= new MajorRepository(context: _context);

            return _majorRepository;
        }
    }

    public IPlatformRepository PlatformRepository
    {
        get
        {
            _platformRepository ??= new PlatformRepository(context: _context);

            return _platformRepository;
        }
    }

    public IBlogCommentRepository BlogCommentRepository
    {
        get
        {
            _blogCommentRepository ??= new BlogCommentRepository(context: _context);

            return _blogCommentRepository;
        }
    }

    public IBlogRepository BlogRepository
    {
        get
        {
            _blogRepository ??= new BlogRepository(context: _context);

            return _blogRepository;
        }
    }

    public IProjectRepository ProjectRepository
    {
        get
        {
            _projectRepository ??= new ProjectRepository(context: _context);

            return _projectRepository;
        }
    }

    public ICvRepository CvRepository
    {
        get
        {
            _cvRepository ??= new CvRepository(context: _context);

            return _cvRepository;
        }
    }

    public async Task CreateTransactionAsync(CancellationToken cancellationToken)
    {
        _dbContextTransaction = await _context.Database.BeginTransactionAsync(cancellationToken: cancellationToken);
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbContextTransaction.CommitAsync(cancellationToken: cancellationToken);
    }

    public Task RollBackTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbContextTransaction.RollbackAsync(cancellationToken: cancellationToken);
    }

    public ValueTask DisposeTransactionAsync()
    {
        return _dbContextTransaction.DisposeAsync();
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }


    public Task SaveToDatabaseAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}
