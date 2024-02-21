using Application.Features.Position.CreatePosition;
using Application.Features.Position.GetAllPositions;
using Application.Features.Position.GetAllPositionsByPositionName;
using Application.Features.Position.GetAllTemporarilyRemovedPositions;
using Application.Features.Position.RemovePositionPermanentlyByPositionId;
using Application.Features.Position.RemovePositionTemporarilyByPositionId;
using Application.Features.Position.RestorePositionByPositionId;
using Application.Features.Position.UpdatePosition;
using Domain.Entities;
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
using WebApi.ApiReturnCodes.Base;
using WebApi.Attributes;
using WebApi.Commons;
using WebApi.DTOs.Position.Incomings;

namespace WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class PositionController : ControllerBase
{
    private readonly ISender _sender;

    public PositionController(ISender sender)
    {
        _sender = sender;
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
        GetAllPositionsRequest request = new()
        {
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            //500
            case GetAllPositionsStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse
                    {
                        Body = response.FoundPositions
                    });
                }
        }
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
            ErrorMessage = $"Less than min length of position name !!")]
                string positionName,
        CancellationToken cancellationToken)
    {
        positionName = positionName.Trim();

        // Find position by name.
        GetAllPositionsByPositionNameRequest request = new()
        {
            PositionName = positionName,
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case GetAllPositionsByPositionNameStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse
                    {
                        Body = response.FoundPositions
                    });
                }
        }
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
        CreatePositionRequest request = new()
        {
            NewPositionName = dto.PositionName
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case CreatePositionStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 400
            case CreatePositionStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Found position with name = {dto.PositionName} in temporarily removed storage."
                        }
                    });
                }

            // 409
            case CreatePositionStatusCode.POSITION_ALREADY_EXISTS:
                {
                    return Conflict(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_ALREADY_EXISTS,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with name = {dto.PositionName} already exists."
                        }
                    });
                }

            // 500
            case CreatePositionStatusCode.DATABASE_OPERATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Database operations failed."
                            }
                        });
                }

            // 201
            default:
                {
                    return Created(
                        uri: $"{HttpContext.Request.Path}?name={dto.PositionName}",
                        value: new CommonResponse { });
                }
        }
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
        RemovePositionTemporarilyByPositionIdRequest request = new()
        {
            PositionId = positionId,
            PositionRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case RemovePositionTemporarilyByPositionIdStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 404
            case RemovePositionTemporarilyByPositionIdStatusCode.POSITION_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with Id = {positionId} is not found."
                        }
                    });
                }

            // 400
            case RemovePositionTemporarilyByPositionIdStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Found position with Id = {positionId} in temporarily removed storage."
                        }
                    });
                }

            // 500
            case RemovePositionTemporarilyByPositionIdStatusCode.DATABASE_OPERATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Database operations failed."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse { });
                }
        }
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
        UpdatePositionByPositionIdRequest request = new()
        {
            NewPositionName = dto.NewPositionName,
            PositionId = positionId,
            PositionUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case UpdatePositionByPositionIdStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 404
            case UpdatePositionByPositionIdStatusCode.POSITION_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with Id = {positionId} is not found."
                        }
                    });
                }

            // 400
            case UpdatePositionByPositionIdStatusCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_ALREADY_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Found position with Id = {positionId} in temporarily removed storage."
                        }
                    });
                }

            // 409
            case UpdatePositionByPositionIdStatusCode.POSITION_ALREADY_EXISTS:
                {
                    return Conflict(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_ALREADY_EXISTS,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with name = {dto.NewPositionName} already exists."
                        }
                    });
                }

            // 500
            case UpdatePositionByPositionIdStatusCode.DATABASE_OPERATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Database operations failed."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse { });
                }
        }
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
        // Get all temporarily removed position.
        GetAllTemporarilyRemovedPositionsRequest request = new()
        {
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case GetAllTemporarilyRemovedPositionsStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse
                    {
                        Body = response.FoundTemporarilyRemovedPositions.Select(position =>
                            new GetAllTemporarilyRemovedPositionsResponse.Position
                            {
                                PositionId = position.PositionId,
                                PositionName = position.PositionName,
                                PositionRemovedAt = TimeZoneInfo.ConvertTimeFromUtc(
                                    dateTime: position.PositionRemovedAt,
                                    destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                        id: "SE Asia Standard Time")),
                                PositionRemovedBy = position.PositionRemovedBy
                            })
                    });
                }
        }
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
        RemovePositionPermanentlyByPositionIdRequest request = new()
        {
            PositionId = positionId,
            PositionRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case RemovePositionPermanentlyByPositionIdStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 404
            case RemovePositionPermanentlyByPositionIdStatusCode.POSITION_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with Id = {positionId} is not found."
                        }
                    });
                }

            // 400
            case RemovePositionPermanentlyByPositionIdStatusCode.POSITION_IS_NOT_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_NOT_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with Id = {positionId} is not found in temporarily removed storage."
                        }
                    });
                }

            // 500
            case RemovePositionPermanentlyByPositionIdStatusCode.DATABASE_OPERATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Database operations failed."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse { });
                }
        }
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
        RestorePositionByPositionIdRequest request = new()
        {
            PositionId = positionId
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case RestorePositionByPositionIdStatusCode.INPUT_VALIDATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Server error. Please try again later."
                            }
                        });
                }

            // 404
            case RestorePositionByPositionIdStatusCode.POSITION_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with Id = {positionId} is not found."
                        }
                    });
                }

            // 400
            case RestorePositionByPositionIdStatusCode.POSITION_IS_NOT_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = PositionApiReturnCode.POSITION_IS_NOT_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Position with Id = {positionId} is not found in temporarily removed storage."
                        }
                    });
                }

            // 500
            case RestorePositionByPositionIdStatusCode.DATABASE_OPERATION_FAIL:
                {
                    return StatusCode(
                        statusCode: StatusCodes.Status500InternalServerError,
                        value: new CommonResponse
                        {
                            ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                            ErrorMessages = new List<string>(capacity: 1)
                            {
                                "Database operations failed."
                            }
                        });
                }

            // 200
            default:
                {
                    return Ok(value: new CommonResponse { });
                }
        }
    }
}
