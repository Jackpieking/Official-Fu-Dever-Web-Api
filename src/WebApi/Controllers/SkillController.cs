using Application.Commons;
using Application.Features.Skill.Commands.CreateSkill;
using Application.Features.Skill.Commands.RemoveSkillTemporarily;
using Application.Features.Skill.Queries.FindBySkillName;
using Application.Features.Skill.Queries.GetAllSkill;
using Application.Features.Skill.Queries.IsSkillFoundBySkillId;
using Application.Features.Skill.Queries.IsSkillFoundBySkillName;
using Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillId;
using Application.Features.Skill.Queries.IsSkillTemporarilyRemovedBySkillName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using WebApi.ApiReturnCodes;
using WebApi.Attributes;
using WebApi.Commons;
using WebApi.DTOs.Skill.Incomings;
using WebApi.DTOs.Skill.Outgoings;

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
    [AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        GetAllSkillQuery getAllSkillQuery = new()
        {
            CacheExpiredTime = 60
        };

        // Get all skills which are not temporarily removed.
        var foundSkills = await _sender.Send(
            request: getAllSkillQuery,
            cancellationToken: cancellationToken);

        return Ok(value: new CommonResponse
        {
            Body = MapToDto()
        });

        // Map the result to dto.
        List<GetSkillDto> MapToDto()
        {
            List<GetSkillDto> dtos = [];

            foreach (var foundSkill in foundSkills)
            {
                dtos.Add(item: new()
                {
                    Id = foundSkill.Id,
                    Name = foundSkill.Name
                });
            }

            dtos.TrimExcess();

            return dtos;
        }
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
    /// <response code="404"></response>
    /// <response code="200"></response>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllByNameAsync(
        [FromQuery(Name = "name")]
        [Required]
        [IsStringNotNull]
            string skillName,
        CancellationToken cancellationToken)
    {
        skillName = skillName.Trim();

        // Find skill by name.
        FindBySkillNameQuery findBySkillNameQuery = new()
        {
            SkillName = skillName,
            CacheExpiredTime = 60
        };

        var foundSkills = await _sender.Send(
            request: findBySkillNameQuery,
            cancellationToken: cancellationToken);

        // Skills with name are not found or are already temporarily removed.
        if (Equals(objA: foundSkills.FirstOrDefault(), objB: default))
        {
            return NotFound(value: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"No skills with name = {skillName} are found."
                }
            });
        }

        return Ok(value: new CommonResponse
        {
            Body = MapToDto()
        });

        // Map the result to dto.
        List<GetSkillDto> MapToDto()
        {
            List<GetSkillDto> dtos = [];

            foreach (var foundSkill in foundSkills)
            {
                dtos.Add(item: new()
                {
                    Id = foundSkill.Id,
                    Name = foundSkill.Name
                });
            }

            dtos.TrimExcess();

            return dtos;
        }
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
    /// <response code="403"></response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateSkillDto dto,
        CancellationToken cancellationToken)
    {
        dto.NormalizeAllProperties();

        // Is skill found by name.
        IsSkillFoundBySkillNameQuery isSkillFoundBySkillNameQuery = new()
        {
            SkillName = dto.SkillName,
            CacheExpiredTime = 60
        };

        var isSKillFound = await _sender.Send(
            request: isSkillFoundBySkillNameQuery,
            cancellationToken: cancellationToken);

        // Skills with name already exists.
        if (isSKillFound)
        {
            // Is skill temporarily removed by name.
            IsSkillTemporarilyRemovedBySkillNameQuery isSkillTemporarilyRemovedBySkillNameQuery = new()
            {
                SkillName = dto.SkillName,
                CacheExpiredTime = 60
            };

            var isSkillTemporarilyRemoved = await _sender.Send(
                request: isSkillTemporarilyRemovedBySkillNameQuery,
                cancellationToken: cancellationToken);

            // Skill with name is temporarily removed.
            if (isSkillTemporarilyRemoved)
            {
                return BadRequest(error: new CommonResponse
                {
                    ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        $"Found skill with name = {dto.SkillName} in temporarily deleted storage."
                    }
                });
            }

            return Conflict(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_ALREADY_EXISTS,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with name = {dto.SkillName} already exists."
                }
            });
        }

        // Adding new skill.
        CreateSkillCommand createSkillCommand = new()
        {
            NewSkillName = dto.SkillName
        };

        var dbResult = await _sender.Send(
            request: createSkillCommand,
            cancellationToken: cancellationToken);

        // New skill cannot be added.
        if (!dbResult)
        {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = ApiReturnCodes.Base.BaseApiReturnCode.FAILED,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                });
        }

        return Created(
            uri: $"{HttpContext.Request.Path}?name={dto.SkillName}",
            value: new CommonResponse
            {
                ApiReturnCode = ApiReturnCodes.Base.BaseApiReturnCode.SUCCESS
            });
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
    /// <response code="403"></response>
    [HttpDelete(template: "{skillId:guid}")]
    public async Task<IActionResult> RemoveTemporarilyByIdAsync(
        [FromRoute]
        [Required]
        [IsGuidNotEmpty]
            Guid skillId,
        CancellationToken cancellationToken)
    {
        // Is skill found by id.
        IsSkillFoundBySkillIdQuery isSkillFoundBySkillIdQuery = new()
        {
            SkillId = skillId,
            CacheExpiredTime = 60
        };

        var isSKillFound = await _sender.Send(
            request: isSkillFoundBySkillIdQuery,
            cancellationToken: cancellationToken);

        // Skill with id is not found.
        if (!isSKillFound)
        {
            return NotFound(value: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_NOT_FOUND,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Skill with Id = {skillId} is not found."
                }
            });
        }

        // Is skill temporarily removed by id.
        IsSkillTemporarilyRemovedBySkillIdQuery isSkillTemporarilyRemovedBySkillIdQuery = new()
        {
            SkillId = skillId,
            CacheExpiredTime = 60
        };

        var isSkillTemporarilyRemoved = await _sender.Send(
            request: isSkillTemporarilyRemovedBySkillIdQuery,
            cancellationToken: cancellationToken);

        // Skill with id is temporarily removed.
        if (isSkillTemporarilyRemoved)
        {
            return BadRequest(error: new CommonResponse
            {
                ApiReturnCode = SkillApiReturnCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
                ErrorMessages = new List<string>(capacity: 1)
                {
                    $"Found skill with Id = {skillId} in temporarily deleted storage."
                }
            });
        }

        // Update found skill.
        RemoveSkillTemporarilyCommand removeSkillTemporarilyCommand = new()
        {
            SkillId = skillId,
            // SkillRemovedBy = Guid.Parse(
            //     input: User.FindFirstValue(
            //         claimType: JwtRegisteredClaimNames.Sub))
            SkillRemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
        };

        var dbResult = await _sender.Send(
            request: removeSkillTemporarilyCommand,
            cancellationToken: cancellationToken);

        // Cannot update found skill.
        if (!dbResult)
        {
            return StatusCode(
                statusCode: StatusCodes.Status500InternalServerError,
                value: new CommonResponse
                {
                    ApiReturnCode = ApiReturnCodes.Base.BaseApiReturnCode.FAILED,
                    ErrorMessages = new List<string>(capacity: 1)
                    {
                        "Database operations failed."
                    }
                });
        }

        return Ok(value: new CommonResponse
        {
        });
    }
}
