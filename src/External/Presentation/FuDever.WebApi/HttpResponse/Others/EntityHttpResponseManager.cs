using FuDever.WebApi.HttpResponse.Entities.Department.Others;

namespace FuDever.WebApi.HttpResponse.Others;

/// <summary>
///     Manage all entity http response manager.
/// </summary>
public sealed class EntityHttpResponseManager
{
    private DepartmentHttpResponseManager _departmentHttpResponseManager;

    internal DepartmentHttpResponseManager Department
    {
        get
        {
            _departmentHttpResponseManager ??= new();

            return _departmentHttpResponseManager;
        }
    }
}
