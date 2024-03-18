using FuDever.WebApi.HttpResponse.Entities.Department.CreateDepartment.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.GetAllDepartments.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.GetAllDepartmentsByDepartmentName.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.GetAllTemporarilyRemovedDepartments.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.RestoreDepartmentByDepartmentId.Others;
using FuDever.WebApi.HttpResponse.Entities.Department.UpdateDepartmentByDepartmentId.Others;

namespace FuDever.WebApi.HttpResponse.Entities.Department.Others;

/// <summary>
///     Handles all HTTP responses for Department.
/// </summary>
internal sealed class DepartmentHttpResponseManager
{
    // Backing fields.
    private GetAllDepartmentsHttpResponseManager
        _getAllDepartmentsHttpResponseManager;
    private GetAllDepartmentsByDepartmentNameHttpResponseManager
        _getAllDepartmentsByDepartmentNameHttpResponseManager;
    private CreateDepartmentHttpResponseManager
        _createDepartmentHttpResponseHttpResponseManager;
    private GetAllTemporarilyRemovedDepartmentsHttpResponseManager
        _getAllTemporarilyRemovedDepartmentsHttpResponseManager;
    private RemoveDepartmentPermanentlyByDepartmentIdHttpResponseManager
        _removeDepartmentPermanentlyByDepartmentIdHttpResponseManager;
    private RemoveDepartmentTemporarilyByDepartmentIdHttpResponseManager
        _removeDepartmentTemporarilyByDepartmentIdHttpResponseManager;
    private UpdateDepartmentByDepartmentIdHttpResponseManager
        _updateDepartmentByDepartmentIdHttpResponseManager;
    private RestoreDepartmentByDepartmentIdHttpResponseManager
        _restoreDepartmentByDepartmentIdHttpResponseManager;

    internal GetAllDepartmentsHttpResponseManager GetAllDepartments
    {
        get
        {
            _getAllDepartmentsHttpResponseManager ??= new();

            return _getAllDepartmentsHttpResponseManager;
        }
    }

    internal GetAllDepartmentsByDepartmentNameHttpResponseManager GetAllDepartmentsByDepartmentName
    {
        get
        {
            _getAllDepartmentsByDepartmentNameHttpResponseManager ??= new();

            return _getAllDepartmentsByDepartmentNameHttpResponseManager;
        }
    }

    internal CreateDepartmentHttpResponseManager CreateDepartment
    {
        get
        {
            _createDepartmentHttpResponseHttpResponseManager ??= new();

            return _createDepartmentHttpResponseHttpResponseManager;
        }
    }

    internal GetAllTemporarilyRemovedDepartmentsHttpResponseManager GetAllTemporarilyRemovedDepartments
    {
        get
        {
            _getAllTemporarilyRemovedDepartmentsHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedDepartmentsHttpResponseManager;
        }
    }

    internal RemoveDepartmentPermanentlyByDepartmentIdHttpResponseManager RemoveDepartmentPermanentlyByDepartmentId
    {
        get
        {
            _removeDepartmentPermanentlyByDepartmentIdHttpResponseManager ??= new();

            return _removeDepartmentPermanentlyByDepartmentIdHttpResponseManager;
        }
    }

    internal RemoveDepartmentTemporarilyByDepartmentIdHttpResponseManager RemoveDepartmentTemporarilyByDepartmentId
    {
        get
        {
            _removeDepartmentTemporarilyByDepartmentIdHttpResponseManager ??= new();

            return _removeDepartmentTemporarilyByDepartmentIdHttpResponseManager;
        }
    }

    internal UpdateDepartmentByDepartmentIdHttpResponseManager UpdateDepartmentByDepartmentId
    {
        get
        {
            _updateDepartmentByDepartmentIdHttpResponseManager ??= new();

            return _updateDepartmentByDepartmentIdHttpResponseManager;
        }
    }

    internal RestoreDepartmentByDepartmentIdHttpResponseManager RestoreDepartmentByDepartmentId
    {
        get
        {
            _restoreDepartmentByDepartmentIdHttpResponseManager ??= new();

            return _restoreDepartmentByDepartmentIdHttpResponseManager;
        }
    }
}
