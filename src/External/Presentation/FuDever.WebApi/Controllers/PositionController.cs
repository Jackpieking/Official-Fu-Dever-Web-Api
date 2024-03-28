using FuDever.WebApi.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Threading;
using FuDever.Domain.Entities;
using FuDever.Application.Features.Position.GetAllPositions;
using FuDever.Application.Features.Position.GetAllPositionsByPositionName;
using FuDever.WebApi.DTOs.Position.Incomings;
using FuDever.Application.Features.Position.CreatePosition;
using FuDever.Application.Features.Position.RemovePositionTemporarilyByPositionId;
using FuDever.Application.Features.Position.UpdatePositionByPositionId;
using FuDever.Application.Features.Position.GetAllTemporarilyRemovedPositions;
using FuDever.Application.Features.Position.RemovePositionPermanentlyByPositionId;
using FuDever.Application.Features.Position.RestorePositionByPositionId;
using FuDever.WebApi.HttpResponseMapper.Others;

namespace FuDever.WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class PositionController : ControllerBase
{
    private readonly ISender _sender;
    private readonly HttpResponseMapperManager _httpResponseMapperManager;

    public PositionController(
        ISender sender,
        HttpResponseMapperManager httpResponseMapperManager)
    {
        _sender = sender;
        _httpResponseMapperManager = httpResponseMapperManager;
    }

    /// <summary>
    ///     Get all positions which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of positions.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Position/all
    ///
    /// </remarks>
    /// <response code="200"></response>
    /// <response code="500"></response>
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        // Get all positions which are not temporarily removed.
        GetAllPositionsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.GetAllPositions
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all positions having name equal to
    ///     input <paramref name="positionName"/> in lowercase.
    /// </summary>
    /// <param name="positionName">
    ///     Use to search for positions with similar name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of positions having name equal to
    ///     input <paramref name="positionName"/>.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Position?name={positionName}
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
            length: Position.Metadata.Name.MaxLength,
            ErrorMessage = "Too much chars on position name !!")]
        [MinLength(
            length: Position.Metadata.Name.MinLength,
            ErrorMessage = "Less than min length of position name !!")]
                string positionName,
        CancellationToken cancellationToken)
    {
        positionName = positionName.Trim();

        // Find position by name.
        GetAllPositionsByPositionNameRequest featureRequest = new()
        {
            PositionName = positionName,
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.GetAllPositionsByPositionName
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Create new position with credentials
    ///     from input <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">
    ///     Containing credentials that are
    ///     used to create a new position.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Position
    ///     {
    ///         "positionName": "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="409"></response>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreatePositionDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Create position
        CreatePositionRequest featureRequest = new()
        {
            NewPositionName = dto.PositionName
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.CreatePosition
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        if (apiResponse.HttpCode == StatusCodes.Status201Created)
        {
            return Created(
                uri: $"{HttpContext.Request.Path}?name={dto.PositionName}",
                value: apiResponse);
        }

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///    Temporarily remove an existed position
    ///    by input <paramref name="positionId"/>.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Position/{positionId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "{positionId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid positionId,
        CancellationToken cancellationToken)
    {
        // Remove position temporarily by position id.
        RemovePositionTemporarilyByPositionIdRequest featureRequest = new()
        {
            PositionId = positionId,
            PositionRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.RemovePositionTemporarilyByPositionId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Update an existed position with credentials.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position to search for.
    /// </param>
    /// <param name="dto">
    ///     Containing credentials that are used to change the found position.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Position/{positionId:guid}
    ///     {
    ///         "newPositionName" : "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    /// <response code="409"></response>
    [HttpPatch(template: "{positionId:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid positionId,
        [FromBody]
            UpdatePositionDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Update position by position id.
        UpdatePositionByPositionIdRequest featureRequest = new()
        {
            NewPositionName = dto.NewPositionName,
            PositionId = positionId,
            PositionUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.UpdatePositionByPositionId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all positions that have been temporarily removed.
    /// </summary>
    /// <returns>
    ///     A list of temporarily removed positions.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Position/remove/all
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [HttpGet(template: "remove/all")]
    public async Task<IActionResult> GetAllTemporarilyRemovedAsync(CancellationToken cancellationToken)
    {
        // Get all temporarily removed positions.
        GetAllTemporarilyRemovedPositionsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.GetAllTemporarilyRemovedPositions
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Permanently remove an existed temporarily removed position.
    ///     by input position id.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Position/remove/{positionId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "remove/{positionId:guid}")]
    public async Task<IActionResult> RemovePermanentlyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid positionId,
        CancellationToken cancellationToken)
    {
        RemovePositionPermanentlyByPositionIdRequest featureRequest = new()
        {
            PositionId = positionId,
            PositionRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.RemovePositionPermanentlyByPositionId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Restore a temporarily removed position by input position id.
    /// </summary>
    /// <param name="positionId">
    ///     Id of position to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Position/remove/{positionId:guid}
    ///
    /// </remarks>
    ///
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpPatch(template: "remove/{positionId:guid}")]
    public async Task<IActionResult> RestoreByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid positionId,
        CancellationToken cancellationToken)
    {
        // Restore position by position id.
        RestorePositionByPositionIdRequest featureRequest = new()
        {
            PositionId = positionId
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Position.RestorePositionByPositionId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
