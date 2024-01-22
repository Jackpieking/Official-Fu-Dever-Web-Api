using Application.Features.Skill.Queries.GetAllSkill;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Common;
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
    //[AllowAnonymous]
    [HttpGet(template: "all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        GetAllSkillQuery getAllSkillQuery = new();

        // Get all skills which are not temporarily removed.
        var foundSkills = await _sender.Send(
            request: getAllSkillQuery,
            cancellationToken: cancellationToken);

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

        return Ok(value: new CommonResponse
        {
            Body = dtos
        });
    }
}
