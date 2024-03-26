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
using FuDever.Domain.Entities;
using FuDever.Application.Features.Hobby.GetAllHobbies;
using FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName;
using FuDever.WebApi.DTOs.Hobby.Incomings;
using FuDever.Application.Features.Hobby.CreateHobby;
using FuDever.Application.Features.Hobby.RemoveHobbyTemporarilyByHobbyId;
using FuDever.Application.Features.Hobby.UpdateHobbyByHobbyId;
using FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;
using FuDever.Application.Features.Hobby.RemoveHobbyPermanentlyByHobbyId;
using FuDever.Application.Features.Hobby.RestoreHobbyByHobbyId;

namespace FuDever.WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class HobbyController : ControllerBase
{
    private readonly ISender _sender;
    private readonly EntityHttpResponseManager _entityHttpResponseManager;

    public HobbyController(
        ISender sender,
        EntityHttpResponseManager entityHttpResponseManager)
    {
        _sender = sender;
        _entityHttpResponseManager = entityHttpResponseManager;
    }

    /// <summary>
    ///     Get all hobbies which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of hobbies.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Hobby/all
    ///
    /// </remarks>
    /// <response code="200"></response>
    /// <response code="500"></response>
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        // Get all hobbies which are not temporarily removed.
        GetAllHobbiesRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.GetAllHobbies
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all hobbies having name equal to
    ///     input <paramref name="hobbyName"/> in lowercase.
    /// </summary>
    /// <param name="hobbyName">
    ///     Use to search for hobbies with similar name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of hobbies having name equal to
    ///     input <paramref name="hobbyName"/>.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Hobby?name={hobbyName}
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
            length: Hobby.Metadata.Name.MaxLength,
            ErrorMessage = "Too much chars on hobby name !!")]
        [MinLength(
            length: Hobby.Metadata.Name.MinLength,
            ErrorMessage = "Less than min length of hobby name !!")]
                string hobbyName,
        CancellationToken cancellationToken)
    {
        hobbyName = hobbyName.Trim();

        // Find hobby by name.
        GetAllHobbiesByHobbyNameRequest featureRequest = new()
        {
            HobbyName = hobbyName,
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.GetAllHobbiesByHobbyName
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Create new hobby with credentials
    ///     from input <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">
    ///     Containing credentials that are
    ///     used to create a new hobby.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Hobby
    ///     {
    ///         "hobbyName": "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="409"></response>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateHobbyDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Create hobby
        CreateHobbyRequest featureRequest = new()
        {
            NewHobbyName = dto.HobbyName
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.CreateHobby
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        if (apiResponse.HttpCode == StatusCodes.Status201Created)
        {
            return Created(
                uri: $"{HttpContext.Request.Path}?name={dto.HobbyName}",
                value: apiResponse);
        }

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///    Temporarily remove an existed hobby
    ///    by input <paramref name="hobbyId"/>.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Hobby/{hobbyId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "{hobbyId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid hobbyId,
        CancellationToken cancellationToken)
    {
        // Remove hobby temporarily by hobby id.
        RemoveHobbyTemporarilyByHobbyIdRequest featureRequest = new()
        {
            HobbyId = hobbyId,
            HobbyRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.RemoveHobbyTemporarilyByHobbyId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Update an existed hobby with credentials.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby to search for.
    /// </param>
    /// <param name="dto">
    ///     Containing credentials that are used to change the found hobby.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Hobby/{hobbyId:guid}
    ///     {
    ///         "newHobbyName" : "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    /// <response code="409"></response>
    [HttpPatch(template: "{hobbyId:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid hobbyId,
        [FromBody]
            UpdateHobbyDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Update hobby by hobby id.
        UpdateHobbyByHobbyIdRequest featureRequest = new()
        {
            NewHobbyName = dto.NewHobbyName,
            HobbyId = hobbyId,
            HobbyUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.UpdateHobbyByHobbyId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all hobbies that have been temporarily removed.
    /// </summary>
    /// <returns>
    ///     A list of temporarily removed hobbies.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Hobby/remove/all
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [HttpGet(template: "remove/all")]
    public async Task<IActionResult> GetAllTemporarilyRemovedAsync(CancellationToken cancellationToken)
    {
        // Get all temporarily removed hobbies.
        GetAllTemporarilyRemovedHobbiesRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.GetAllTemporarilyRemovedHobbies
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Permanently remove an existed temporarily removed hobby.
    ///     by input hobby id.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Hobby/remove/{hobbyId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "remove/{hobbyId:guid}")]
    public async Task<IActionResult> RemovePermanentlyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid hobbyId,
        CancellationToken cancellationToken)
    {
        RemoveHobbyPermanentlyByHobbyIdRequest featureRequest = new()
        {
            HobbyId = hobbyId,
            HobbyRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.RemoveHobbyPermanentlyByHobbyId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Restore a temporarily removed hobby by input hobby id.
    /// </summary>
    /// <param name="hobbyId">
    ///     Id of hobby to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Hobby/remove/{hobbyId:guid}
    ///
    /// </remarks>
    ///
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpPatch(template: "remove/{hobbyId:guid}")]
    public async Task<IActionResult> RestoreByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid hobbyId,
        CancellationToken cancellationToken)
    {
        // Restore hobby by hobby id.
        RestoreHobbyByHobbyIdRequest featureRequest = new()
        {
            HobbyId = hobbyId
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Hobby.RestoreHobbyByHobbyId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
