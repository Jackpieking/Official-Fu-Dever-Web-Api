using FuDever.WebApi.Attributes;
using FuDever.WebApi.HttpResponseMapper.Others;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Threading;
using FuDever.Application.Features.Major.GetAllMajors;
using FuDever.Domain.Entities;
using FuDever.Application.Features.Major.GetAllMajorsByMajorName;
using FuDever.WebApi.DTOs.Major.Incomings;
using FuDever.Application.Features.Major.CreateMajor;
using FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId;
using FuDever.Application.Features.Major.UpdateMajorByMajorId;
using FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;
using FuDever.Application.Features.Major.RemoveMajorPermanentlyByMajorId;
using FuDever.Application.Features.Major.RestoreMajorByMajorId;

namespace FuDever.WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class MajorController : ControllerBase
{
    private readonly ISender _sender;
    private readonly HttpResponseMapperManager _httpResponseMapperManager;

    public MajorController(
        ISender sender,
        HttpResponseMapperManager httpResponseMapperManager)
    {
        _sender = sender;
        _httpResponseMapperManager = httpResponseMapperManager;
    }

    /// <summary>
    ///     Get all majors which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of majors.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Major/all
    ///
    /// </remarks>
    /// <response code="200"></response>
    /// <response code="500"></response>
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        // Get all majors which are not temporarily removed.
        GetAllMajorsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.GetAllMajors
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all majors having name equal to
    ///     input <paramref name="majorName"/> in lowercase.
    /// </summary>
    /// <param name="majorName">
    ///     Use to search for majors with similar name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of majors having name equal to
    ///     input <paramref name="majorName"/>.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Major?name={majorName}
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
            length: Major.Metadata.Name.MaxLength,
            ErrorMessage = "Too much chars on major name !!")]
        [MinLength(
            length: Major.Metadata.Name.MinLength,
            ErrorMessage = "Less than min length of major name !!")]
                string majorName,
        CancellationToken cancellationToken)
    {
        majorName = majorName.Trim();

        // Find major by name.
        GetAllMajorsByMajorNameRequest featureRequest = new()
        {
            MajorName = majorName,
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.GetAllMajorsByMajorName
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Create new major with credentials
    ///     from input <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">
    ///     Containing credentials that are
    ///     used to create a new major.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Major
    ///     {
    ///         "majorName": "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="409"></response>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateMajorDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Create major
        CreateMajorRequest featureRequest = new()
        {
            NewMajorName = dto.MajorName
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.CreateMajor
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        if (apiResponse.HttpCode == StatusCodes.Status201Created)
        {
            return Created(
                uri: $"{HttpContext.Request.Path}?name={dto.MajorName}",
                value: apiResponse);
        }

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///    Temporarily remove an existed major
    ///    by input <paramref name="majorId"/>.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Major/{majorId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "{majorId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid majorId,
        CancellationToken cancellationToken)
    {
        // Remove major temporarily by major id.
        RemoveMajorTemporarilyByMajorIdRequest featureRequest = new()
        {
            MajorId = majorId,
            MajorRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.RemoveMajorTemporarilyByMajorId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Update an existed major with credentials.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major to search for.
    /// </param>
    /// <param name="dto">
    ///     Containing credentials that are used to change the found major.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Major/{majorId:guid}
    ///     {
    ///         "newMajorName" : "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    /// <response code="409"></response>
    [HttpPatch(template: "{majorId:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid majorId,
        [FromBody]
            UpdateMajorDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Update major by major id.
        UpdateMajorByMajorIdRequest featureRequest = new()
        {
            NewMajorName = dto.NewMajorName,
            MajorId = majorId,
            MajorUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.UpdateMajorByMajorId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all majors that have been temporarily removed.
    /// </summary>
    /// <returns>
    ///     A list of temporarily removed majors.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Major/remove/all
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [HttpGet(template: "remove/all")]
    public async Task<IActionResult> GetAllTemporarilyRemovedAsync(CancellationToken cancellationToken)
    {
        // Get all temporarily removed majors.
        GetAllTemporarilyRemovedMajorsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.GetAllTemporarilyRemovedMajors
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Permanently remove an existed temporarily removed major.
    ///     by input major id.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Major/remove/{majorId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "remove/{majorId:guid}")]
    public async Task<IActionResult> RemovePermanentlyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid majorId,
        CancellationToken cancellationToken)
    {
        RemoveMajorPermanentlyByMajorIdRequest featureRequest = new()
        {
            MajorId = majorId,
            MajorRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.RemoveMajorPermanentlyByMajorId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Restore a temporarily removed major by input major id.
    /// </summary>
    /// <param name="majorId">
    ///     Id of major to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Major/remove/{majorId:guid}
    ///
    /// </remarks>
    ///
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpPatch(template: "remove/{majorId:guid}")]
    public async Task<IActionResult> RestoreByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid majorId,
        CancellationToken cancellationToken)
    {
        // Restore major by major id.
        RestoreMajorByMajorIdRequest featureRequest = new()
        {
            MajorId = majorId
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Major.RestoreMajorByMajorId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
