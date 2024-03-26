using FuDever.WebApi.EntityHttpResponse.Entities.Role.CreateRole.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllRoles.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllRolesByRoleName.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllTemporarilyRemovedRoles.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRolePermanentlyByRoleId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.RemoveRoleTemporarilyByRoleId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.RestoreRoleByRoleId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.UpdateRoleByRoleId.Others;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.Others;

/// <summary>
///     Handles all HTTP responses for role.
/// </summary>
internal sealed class RoleHttpResponseManager
{
    // Backing fields.
    private GetAllRolesHttpResponseManager
        _getAllRolesHttpResponseManager;
    private GetAllRolesByRoleNameHttpResponseManager
        _getAllRolesByRoleNameHttpResponseManager;
    private CreateRoleHttpResponseManager
        _createRoleHttpResponseManager;
    private GetAllTemporarilyRemovedRolesHttpResponseManager
        _getAllTemporarilyRemovedRolesHttpResponseManager;
    private RemoveRolePermanentlyByRoleIdHttpResponseManager
        _removeRolePermanentlyByRoleIdHttpResponseManager;
    private RemoveRoleTemporarilyByRoleIdHttpResponseManager
        _removeRoleTemporarilyByRoleIdHttpResponseManager;
    private UpdateRoleByRoleIdHttpResponseManager
        _roleByRoleIdHttpResponseManager;
    private RestoreRoleByRoleIdHttpResponseManager
        _restoreRoleByRoleIdHttpResponseManager;

    internal GetAllRolesHttpResponseManager GetAllRoles
    {
        get
        {
            _getAllRolesHttpResponseManager ??= new();

            return _getAllRolesHttpResponseManager;
        }
    }

    internal GetAllRolesByRoleNameHttpResponseManager GetAllRolesByRoleName
    {
        get
        {
            _getAllRolesByRoleNameHttpResponseManager ??= new();

            return _getAllRolesByRoleNameHttpResponseManager;
        }
    }

    internal CreateRoleHttpResponseManager CreateRole
    {
        get
        {
            _createRoleHttpResponseManager ??= new();

            return _createRoleHttpResponseManager;
        }
    }

    internal GetAllTemporarilyRemovedRolesHttpResponseManager GetAllTemporarilyRemovedRoles
    {
        get
        {
            _getAllTemporarilyRemovedRolesHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedRolesHttpResponseManager;
        }
    }

    internal RemoveRolePermanentlyByRoleIdHttpResponseManager RemoveRolePermanentlyByRoleId
    {
        get
        {
            _removeRolePermanentlyByRoleIdHttpResponseManager ??= new();

            return _removeRolePermanentlyByRoleIdHttpResponseManager;
        }
    }

    internal RemoveRoleTemporarilyByRoleIdHttpResponseManager RemoveRoleTemporarilyByRoleId
    {
        get
        {
            _removeRoleTemporarilyByRoleIdHttpResponseManager ??= new();

            return _removeRoleTemporarilyByRoleIdHttpResponseManager;
        }
    }

    internal UpdateRoleByRoleIdHttpResponseManager RoleByRoleId
    {
        get
        {
            _roleByRoleIdHttpResponseManager ??= new();

            return _roleByRoleIdHttpResponseManager;
        }
    }

    internal RestoreRoleByRoleIdHttpResponseManager RestoreRoleByRoleId
    {
        get
        {
            _restoreRoleByRoleIdHttpResponseManager ??= new();

            return _restoreRoleByRoleIdHttpResponseManager;
        }
    }
}
