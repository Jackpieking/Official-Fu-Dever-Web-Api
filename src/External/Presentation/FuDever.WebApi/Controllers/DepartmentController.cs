using FuDever.Application.Features.Department.CreateDepartment;
using FuDever.Application.Features.Department.GetAllDepartments;
using FuDever.Application.Features.Department.GetAllDepartmentsByDepartmentName;
using FuDever.Application.Features.Department.GetAllTemporarilyRemovedDepartments;
using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;
using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;
using FuDever.Application.Features.Department.RestoreDepartmentByDepartmentId;
using FuDever.Application.Features.Department.UpdateDepartmentByDepartmentId;
using FuDever.Domain.Entities;
using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Department.Incomings;
using FuDever.WebApi.HttpResponse.Others;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class DepartmentController : ControllerBase
{
    private readonly ISender _sender;
    private readonly EntityHttpResponseManager _entityHttpResponseManager;

    public DepartmentController(
        ISender sender,
        EntityHttpResponseManager entityHttpResponseManager)
    {
        _sender = sender;
        _entityHttpResponseManager = entityHttpResponseManager;
    }

    /// <summary>
    ///     Get all departments which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of departments.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Department/all
    ///
    /// </remarks>
    /// <response code="200"></response>
    /// <response code="500"></response>
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        // Get all departments which are not temporarily removed.
        GetAllDepartmentsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.GetAllDepartments
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all departments having name equal to
    ///     input <paramref name="departmentName"/> in lowercase.
    /// </summary>
    /// <param name="departmentName">
    ///     Use to search for departments with similar name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of departments having name equal to
    ///     input <paramref name="departmentName"/>.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Department?name={departmentName}
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
            length: Department.Metadata.Name.MaxLength,
            ErrorMessage = "Too much chars on department name !!")]
        [MinLength(
            length: Department.Metadata.Name.MinLength,
            ErrorMessage = "Less than min length of department name !!")]
                string departmentName,
        CancellationToken cancellationToken)
    {
        departmentName = departmentName.Trim();

        // Find department by name.
        GetAllDepartmentsByDepartmentNameRequest featureRequest = new()
        {
            DepartmentName = departmentName,
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.GetAllDepartmentsByDepartmentName
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Create new department with credentials
    ///     from input <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">
    ///     Containing credentials that are
    ///     used to create a new department.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Department
    ///     {
    ///         "departmentName": "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="409"></response>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateDepartmentDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Create department
        CreateDepartmentRequest featureRequest = new()
        {
            NewDepartmentName = dto.DepartmentName
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.CreateDepartment
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        if (apiResponse.HttpCode == StatusCodes.Status201Created)
        {
            return Created(
                uri: $"{HttpContext.Request.Path}?name={dto.DepartmentName}",
                value: apiResponse);
        }

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///    Temporarily remove an existed department
    ///    by input <paramref name="departmentId"/>.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Department/{departmentId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "{departmentId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid departmentId,
        CancellationToken cancellationToken)
    {
        // Remove department temporarily by department id.
        RemoveDepartmentTemporarilyByDepartmentIdRequest featureRequest = new()
        {
            DepartmentId = departmentId,
            DepartmentRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.RemoveDepartmentTemporarilyByDepartmentId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Update an existed department with credentials.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department to search for.
    /// </param>
    /// <param name="dto">
    ///     Containing credentials that are used to change the found department.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Department/{departmentId:guid}
    ///     {
    ///         "newDepartmentName" : "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    /// <response code="409"></response>
    [HttpPatch(template: "{departmentId:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid departmentId,
        [FromBody]
            UpdateDepartmentDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Update department by department id.
        UpdateDepartmentByDepartmentIdRequest featureRequest = new()
        {
            NewDepartmentName = dto.NewDepartmentName,
            DepartmentId = departmentId,
            DepartmentUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.UpdateDepartmentByDepartmentId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all departments that have been temporarily removed.
    /// </summary>
    /// <returns>
    ///     A list of temporarily removed departments.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Department/remove/all
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [HttpGet(template: "remove/all")]
    public async Task<IActionResult> GetAllTemporarilyRemovedAsync(CancellationToken cancellationToken)
    {
        // Get all temporarily removed department.
        GetAllTemporarilyRemovedDepartmentsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.GetAllTemporarilyRemovedDepartments
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Permanently remove an existed temporarily removed department.
    ///     by input department id.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Department/remove/{departmentId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "remove/{departmentId:guid}")]
    public async Task<IActionResult> RemovePermanentlyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid departmentId,
        CancellationToken cancellationToken)
    {
        RemoveDepartmentPermanentlyByDepartmentIdRequest featureRequest = new()
        {
            DepartmentId = departmentId,
            DepartmentRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.RemoveDepartmentPermanentlyByDepartmentId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Restore a temporarily removed department by input department id.
    /// </summary>
    /// <param name="departmentId">
    ///     Id of department to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Department/remove/{departmentId:guid}
    ///
    /// </remarks>
    ///
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpPatch(template: "remove/{departmentId:guid}")]
    public async Task<IActionResult> RestoreByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid departmentId,
        CancellationToken cancellationToken)
    {
        // Restore department by department id.
        RestoreDepartmentByDepartmentIdRequest featureRequest = new()
        {
            DepartmentId = departmentId
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Department.RestoreDepartmentByDepartmentId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
