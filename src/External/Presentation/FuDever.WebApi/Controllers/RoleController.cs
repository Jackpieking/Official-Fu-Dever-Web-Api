using FuDever.WebApi.Attributes;
using FuDever.WebApi.EntityHttpResponse.Others;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Threading;
using FuDever.Application.Features.Role.GetAllRoles;
using FuDever.Domain.Entities;
using FuDever.Application.Features.Role.GetAllRolesByRoleName;
using FuDever.WebApi.DTOs.Role.Incomings;
using FuDever.Application.Features.Role.CreateRole;
using FuDever.Application.Features.Role.RemoveRoleTemporarilyByRoleId;
using FuDever.Application.Features.Role.UpdateRoleByRoleId;
using FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;
using FuDever.Application.Features.Role.RemoveRolePermanentlyByRoleId;
using FuDever.Application.Features.Role.RestoreRoleByRoleId;

namespace FuDever.WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class RoleController : ControllerBase
{
    private readonly ISender _sender;
    private readonly EntityHttpResponseManager _entityHttpResponseManager;

    public RoleController(
        ISender sender,
        EntityHttpResponseManager entityHttpResponseManager)
    {
        _sender = sender;
        _entityHttpResponseManager = entityHttpResponseManager;
    }

    /// <summary>
    ///     Get all roles which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of roles.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Role/all
    ///
    /// </remarks>
    /// <response code="200"></response>
    /// <response code="500"></response>
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        // Get all roles which are not temporarily removed.
        GetAllRolesRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.GetAllRoles
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all roles having name equal to
    ///     input <paramref name="roleName"/> in lowercase.
    /// </summary>
    /// <param name="roleName">
    ///     Use to search for roles with similar name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of roles having name equal to
    ///     input <paramref name="roleName"/>.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Role?name={roleName}
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllByNameAsync(
        [FromQuery(Name = "name")]
        [Required]
        [StringIsNotNullOrWhiteSpace]
        [MaxLength(
            length: Role.Metadata.Name.MaxLength,
            ErrorMessage = "Too much chars on role name !!")]
        [MinLength(
            length: Role.Metadata.Name.MinLength,
            ErrorMessage = "Less than min length of role name !!")]
                string roleName,
        CancellationToken cancellationToken)
    {
        roleName = roleName.Trim();

        // Find role by name.
        GetAllRolesByRoleNameRequest featureRequest = new()
        {
            RoleName = roleName,
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.GetAllRolesByRoleName
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Create new role with credentials
    ///     from input <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">
    ///     Containing credentials that are
    ///     used to create a new role.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Role
    ///     {
    ///         "roleName": "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="409"></response>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateRoleDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Create role
        CreateRoleRequest featureRequest = new()
        {
            NewRoleName = dto.RoleName
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.CreateRole
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        if (apiResponse.HttpCode == StatusCodes.Status201Created)
        {
            return Created(
                uri: $"{HttpContext.Request.Path}?name={dto.RoleName}",
                value: apiResponse);
        }

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///    Temporarily remove an existed role
    ///    by input <paramref name="roleId"/>.
    /// </summary>
    /// <param name="roleId">
    ///     Id of role to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Role/{roleId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "{roleId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid roleId,
        CancellationToken cancellationToken)
    {
        // Remove role temporarily by role id.
        RemoveRoleTemporarilyByRoleIdRequest featureRequest = new()
        {
            RoleId = roleId,
            RoleRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.RemoveRoleTemporarilyByRoleId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Update an existed role with credentials.
    /// </summary>
    /// <param name="roleId">
    ///     Id of role to search for.
    /// </param>
    /// <param name="dto">
    ///     Containing credentials that are used to change the found role.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Role/{roleId:guid}
    ///     {
    ///         "newRoleName" : "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    /// <response code="409"></response>
    [HttpPatch(template: "{roleId:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid roleId,
        [FromBody]
            UpdateRoleDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Update role by role id.
        UpdateRoleByRoleIdRequest featureRequest = new()
        {
            NewRoleName = dto.NewRoleName,
            RoleId = roleId,
            RoleUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.UpdateRoleByRoleId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all roles that have been temporarily removed.
    /// </summary>
    /// <returns>
    ///     A list of temporarily removed roles.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Role/remove/all
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [HttpGet(template: "remove/all")]
    public async Task<IActionResult> GetAllTemporarilyRemovedAsync(CancellationToken cancellationToken)
    {
        // Get all temporarily removed roles.
        GetAllTemporarilyRemovedRolesRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.GetAllTemporarilyRemovedRoles
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Permanently remove an existed temporarily removed role.
    ///     by input role id.
    /// </summary>
    /// <param name="roleId">
    ///     Id of role to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Role/remove/{roleId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "remove/{roleId:guid}")]
    public async Task<IActionResult> RemovePermanentlyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid roleId,
        CancellationToken cancellationToken)
    {
        RemoveRolePermanentlyByRoleIdRequest featureRequest = new()
        {
            RoleId = roleId,
            RoleRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.RemoveRolePermanentlyByRoleId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Restore a temporarily removed role by input role id.
    /// </summary>
    /// <param name="roleId">
    ///     Id of role to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Role/remove/{roleId:guid}
    ///
    /// </remarks>
    ///
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpPatch(template: "remove/{roleId:guid}")]
    public async Task<IActionResult> RestoreByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid roleId,
        CancellationToken cancellationToken)
    {
        // Restore role by role id.
        RestoreRoleByRoleIdRequest featureRequest = new()
        {
            RoleId = roleId
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Role.RestoreRoleByRoleId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
