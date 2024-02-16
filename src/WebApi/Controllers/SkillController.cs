using Application.Features.Skill.CreateSkill;
using Application.Features.Skill.GetAllSkills;
using Application.Features.Skill.GetAllSkillsByName;
using Application.Features.Skill.GetAllSkillsBySkillName;
using Application.Features.Skill.GetAllTemporarilyRemovedSkills;
using Application.Features.Skill.RemoveSkillPermanentlyBySkillId;
using Application.Features.Skill.RemoveSkillTemporarilyBySkillId;
using Application.Features.Skill.RestoreSkillBySkillId;
using Application.Features.Skill.UpdateSkillBySkillId;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using WebApi.ApiReturnCodes;
using WebApi.ApiReturnCodes.Base;
using WebApi.Attributes;
using WebApi.Commons;
using WebApi.DTOs.Skill.Incomings;

namespace WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class SkillController : ControllerBase
{
    private readonly ISender _sender;

    public SkillController(ISender sender)
    {
        _sender = sender;
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
        GetAllSkillsRequest request = new()
        {
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            GetAllSkillsStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 200
            _ => Ok(value: new CommonResponse
            {
                Body = response.FoundSkills
            }),
        };
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
            ErrorMessage = $"Less than min length of skill name !!")]
                string skillName,
        CancellationToken cancellationToken)
    {
        skillName = skillName.Trim();

        // Find skill by name.
        GetAllSkillsBySkillNameRequest request = new()
        {
            SkillName = skillName,
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            GetAllSkillsBySkillNameStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 200
            _ => Ok(value: new CommonResponse
            {
                Body = response.FoundSkills
            })
        };
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
    ///         "SkillName": "string"
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
        CreateSkillRequest request = new()
        {
            NewSkillName = dto.SkillName
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            CreateSkillStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 400
            CreateSkillStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED => BadRequest(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                ErrorMessages = new List<string>(capacity: 1)
                    {
                        $"Found skill with name = {dto.SkillName} in temporarily removed storage."
                    }
            }),

            // 409
            CreateSkillStatusCode.SKILL_ALREADY_EXISTS => Conflict(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_ALREADY_EXISTS,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with name = {dto.SkillName} already exists."
                }
            }),

            // 500
            CreateSkillStatusCode.DATABASE_OPERATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                }),

            // 201
            _ => Created(
            uri: $"{HttpContext.Request.Path}?name={dto.SkillName}",
            value: new CommonResponse
            {
            })
        };
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
        RemoveSkillTemporarilyBySkillIdRequest request = new()
        {
            SkillId = skillId,
            SkillRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            RemoveSkillTemporarilyBySkillIdStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 404
            RemoveSkillTemporarilyBySkillIdStatusCode.SKILL_IS_NOT_FOUND => NotFound(value: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found."
                }
            }),

            // 400
            RemoveSkillTemporarilyBySkillIdStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED => BadRequest(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Found skill with Id = {skillId} in temporarily removed storage."
                }
            }),

            // 500
            RemoveSkillTemporarilyBySkillIdStatusCode.DATABASE_OPERATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                }),

            // 200
            _ => Ok(value: new CommonResponse
            {
            })
        };
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
        UpdateSkillBySkillIdRequest request = new()
        {
            NewSkillName = dto.NewSkillName,
            SkillId = skillId,
            SkillUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            UpdateSkillBySkillIdStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 404
            UpdateSkillBySkillIdStatusCode.SKILL_IS_NOT_FOUND => NotFound(value: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found."
                }
            }),

            // 400
            UpdateSkillBySkillIdStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED => BadRequest(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Found skill with Id = {skillId} in temporarily removed storage."
                }
            }),

            // 409
            UpdateSkillBySkillIdStatusCode.SKILL_ALREADY_EXISTS => Conflict(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_ALREADY_EXISTS,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with name = {dto.NewSkillName} already exists."
                }
            }),

            // 500
            UpdateSkillBySkillIdStatusCode.DATABASE_OPERATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                }),

            // 200
            _ => Ok(new CommonResponse
            {
            })
        };
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
        // Get all temporarily removed skill.
        GetAllTemporarilyRemovedSkillsRequest request = new()
        {
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            GetAllTemporarilyRemovedSkillsStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 200
            _ => Ok(new CommonResponse
            {
                Body = response.FoundTemporarilyRemovedSkills
            })
        };
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
        RemoveSkillPermanentlyBySkillIdRequest request = new()
        {
            SkillId = skillId,
            SkillRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        return response.StatusCode switch
        {
            // 500
            RemoveSkillPermanentlyBySkillIdStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 404
            RemoveSkillPermanentlyBySkillIdStatusCode.SKILL_IS_NOT_FOUND => NotFound(value: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found."
                }
            }),

            // 400
            RemoveSkillPermanentlyBySkillIdStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED => BadRequest(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found in temporarily removed storage."
                }
            }),

            // 500
            RemoveSkillPermanentlyBySkillIdStatusCode.DATABASE_OPERATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                }),

            // 200
            _ => Ok(new CommonResponse
            {
            })
        };
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
        RestoreSkillBySkillIdRequest request = new()
        {
            SkillId = skillId
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);


        return response.StatusCode switch
        {
            // 500
            RestoreSkillBySkillIdStatusCode.INPUT_VALIDATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Server error. Please try again later."
                    }
                }),

            // 404
            RestoreSkillBySkillIdStatusCode.SKILL_IS_NOT_FOUND => NotFound(value: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found."
                }
            }),

            // 400
            RestoreSkillBySkillIdStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED => BadRequest(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found in temporarily removed storage."
                }
            }),

            // 500
            RestoreSkillBySkillIdStatusCode.DATABASE_OPERATION_FAIL => StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                }),

            // 200
            _ => Ok(new CommonResponse
            {
            })
        };
    }
}