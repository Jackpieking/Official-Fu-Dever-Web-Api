using FuDever.Application.Features.Skill.CreateSkill;
using FuDever.Application.Features.Skill.GetAllSkills;
using FuDever.Application.Features.Skill.GetAllSkillsBySkillName;
using FuDever.Application.Features.Skill.GetAllTemporarilyRemovedSkills;
using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;
using FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId;
using FuDever.Application.Features.Skill.RestoreSkillBySkillId;
using FuDever.Application.Features.Skill.UpdateSkillBySkillId;
using FuDever.Domain.Entities;
using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Skill.Incomings;
using FuDever.WebApi.EntityHttpResponse.Others;
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
public sealed class SkillController : ControllerBase
{
    private readonly ISender _sender;
    private readonly EntityHttpResponseManager _entityHttpResponseManager;

    public SkillController(
        ISender sender,
        EntityHttpResponseManager entityHttpResponseManager)
    {
        _sender = sender;
        _entityHttpResponseManager = entityHttpResponseManager;
    }

    /// <summary>
    ///     Get all skills which are not temporarily removed.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of skills.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Skill/all
    ///
    /// </remarks>
    /// <response code="200"></response>
    /// <response code="500"></response>
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        // Get all skills which are not temporarily removed.
        GetAllSkillsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.GetAllSkills
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all skills having name equal to
    ///     input <paramref name="skillName"/> in lowercase.
    /// </summary>
    /// <param name="skillName">
    ///     Use to search for skills with similar name.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     List of skills having name equal to
    ///     input <paramref name="skillName"/>.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Skill?name={skillName}
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
            length: Skill.Metadata.Name.MaxLength,
            ErrorMessage = "Too much chars on skill name !!")]
        [MinLength(
            length: Skill.Metadata.Name.MinLength,
            ErrorMessage = "Less than min length of skill name !!")]
                string skillName,
        CancellationToken cancellationToken)
    {
        skillName = skillName.Trim();

        // Find skill by name.
        GetAllSkillsBySkillNameRequest featureRequest = new()
        {
            SkillName = skillName,
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.GetAllSkillsBySkillName
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Create new skill with credentials
    ///     from input <paramref name="dto"/>.
    /// </summary>
    /// <param name="dto">
    ///     Containing credentials that are
    ///     used to create a new skill.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Skill
    ///     {
    ///         "skillName": "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="500"></response>
    /// <response code="409"></response>
    /// <response code="201"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateSkillDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Create skill
        CreateSkillRequest featureRequest = new()
        {
            NewSkillName = dto.SkillName
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.CreateSkill
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        if (apiResponse.HttpCode == StatusCodes.Status201Created)
        {
            return Created(
                uri: $"{HttpContext.Request.Path}?name={dto.SkillName}",
                value: apiResponse);
        }

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///    Temporarily remove an existed skill
    ///    by input <paramref name="skillId"/>.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used to notify the system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Skill/{skillId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "{skillId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid skillId,
        CancellationToken cancellationToken)
    {
        // Remove skill temporarily by skill id.
        RemoveSkillTemporarilyBySkillIdRequest featureRequest = new()
        {
            SkillId = skillId,
            SkillRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.RemoveSkillTemporarilyBySkillId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Update an existed skill with credentials.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill to search for.
    /// </param>
    /// <param name="dto">
    ///     Containing credentials that are used to change the found skill.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Skill/{skillId:guid}
    ///     {
    ///         "newSkillName" : "string"
    ///     }
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    /// <response code="409"></response>
    [HttpPatch(template: "{skillId:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid skillId,
        [FromBody]
            UpdateSkillDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Update skill by skill id.
        UpdateSkillBySkillIdRequest featureRequest = new()
        {
            NewSkillName = dto.NewSkillName,
            SkillId = skillId,
            SkillUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.UpdateSkillBySkillId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Get all skills that have been temporarily removed.
    /// </summary>
    /// <returns>
    ///     A list of temporarily removed skills.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET api/Skill/remove/all
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="200"></response>
    [HttpGet(template: "remove/all")]
    public async Task<IActionResult> GetAllTemporarilyRemovedAsync(CancellationToken cancellationToken)
    {
        // Get all temporarily removed skills.
        GetAllTemporarilyRemovedSkillsRequest featureRequest = new()
        {
            CacheExpiredTime = 60
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.GetAllTemporarilyRemovedSkills
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Permanently remove an existed temporarily removed skill.
    ///     by input skill id.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE api/Skill/remove/{skillId:guid}
    ///
    /// </remarks>
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpDelete(template: "remove/{skillId:guid}")]
    public async Task<IActionResult> RemovePermanentlyByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid skillId,
        CancellationToken cancellationToken)
    {
        RemoveSkillPermanentlyBySkillIdRequest featureRequest = new()
        {
            SkillId = skillId,
            SkillRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.RemoveSkillPermanentlyBySkillId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }

    /// <summary>
    ///     Restore a temporarily removed skill by input skill id.
    /// </summary>
    /// <param name="skillId">
    ///     Id of skill to search for.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH api/Skill/remove/{skillId:guid}
    ///
    /// </remarks>
    ///
    /// <response code="500"></response>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="200"></response>
    [HttpPatch(template: "remove/{skillId:guid}")]
    public async Task<IActionResult> RestoreByIdAsync(
        [FromRoute]
        [Required]
        [GuidIsNotEmpty]
            Guid skillId,
        CancellationToken cancellationToken)
    {
        // Restore skill by skill id.
        RestoreSkillBySkillIdRequest featureRequest = new()
        {
            SkillId = skillId
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _entityHttpResponseManager.Skill.RestoreSkillBySkillId
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
