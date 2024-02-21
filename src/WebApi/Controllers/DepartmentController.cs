using Application.Features.Department.CreateDepartment;
using Application.Features.Department.GetAllDepartments;
using Application.Features.Department.GetAllDepartmentsByDepartmentName;
using Application.Features.Department.GetAllTemporarilyRemovedDepartments;
using Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;
using Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;
using Application.Features.Department.RestoreDepartmentByDepartmentId;
using Application.Features.Department.UpdateDepartmentByDepartmentId;
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
using WebApi.DTOs.Department.Incomings;

namespace WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class DepartmentController : ControllerBase
{
    private readonly ISender _sender;

    public DepartmentController(ISender sender)
    {
        _sender = sender;
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
        GetAllDepartmentsRequest request = new()
        {
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            //500
            case GetAllDepartmentsStatusCode.INPUT_VALIDATION_FAIL:
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
                        Body = response.FoundDepartments
                    });
                }
        }
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
            ErrorMessage = $"Less than min length of department name !!")]
                string departmentName,
        CancellationToken cancellationToken)
    {
        departmentName = departmentName.Trim();

        // Find department by name.
        GetAllDepartmentsByDepartmentNameRequest request = new()
        {
            DepartmentName = departmentName,
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case GetAllDepartmentsByDepartmentNameStatusCode.INPUT_VALIDATION_FAIL:
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
                        Body = response.FoundDepartments
                    });
                }
        }
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
        CreateDepartmentRequest request = new()
        {
            NewDepartmentName = dto.DepartmentName
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case CreateDepartmentStatusCode.INPUT_VALIDATION_FAIL:
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
            case CreateDepartmentStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Found department with name = {dto.DepartmentName} in temporarily removed storage."
                        }
                    });
                }

            // 409
            case CreateDepartmentStatusCode.DEPARTMENT_ALREADY_EXISTS:
                {
                    return Conflict(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_ALREADY_EXISTS,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with name = {dto.DepartmentName} already exists."
                        }
                    });
                }

            // 500
            case CreateDepartmentStatusCode.DATABASE_OPERATION_FAIL:
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
                        uri: $"{HttpContext.Request.Path}?name={dto.DepartmentName}",
                        value: new CommonResponse { });
                }
        }
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
        RemoveDepartmentTemporarilyByDepartmentIdRequest request = new()
        {
            DepartmentId = departmentId,
            DepartmentRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case RemoveDepartmentTemporarilyByDepartmentIdStatusCode.INPUT_VALIDATION_FAIL:
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
            case RemoveDepartmentTemporarilyByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with Id = {departmentId} is not found."
                        }
                    });
                }

            // 400
            case RemoveDepartmentTemporarilyByDepartmentIdStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Found department with Id = {departmentId} in temporarily removed storage."
                        }
                    });
                }

            // 500
            case RemoveDepartmentTemporarilyByDepartmentIdStatusCode.DATABASE_OPERATION_FAIL:
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
        UpdateDepartmentByDepartmentIdRequest request = new()
        {
            NewDepartmentName = dto.NewDepartmentName,
            DepartmentId = departmentId,
            DepartmentUpdatedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case UpdateDepartmentByDepartmentIdStatusCode.INPUT_VALIDATION_FAIL:
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
            case UpdateDepartmentByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with Id = {departmentId} is not found."
                        }
                    });
                }

            // 400
            case UpdateDepartmentByDepartmentIdStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Found department with Id = {departmentId} in temporarily removed storage."
                        }
                    });
                }

            // 409
            case UpdateDepartmentByDepartmentIdStatusCode.DEPARTMENT_ALREADY_EXISTS:
                {
                    return Conflict(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_ALREADY_EXISTS,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with name = {dto.NewDepartmentName} already exists."
                        }
                    });
                }

            // 500
            case UpdateDepartmentByDepartmentIdStatusCode.DATABASE_OPERATION_FAIL:
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
        GetAllTemporarilyRemovedDepartmentsRequest request = new()
        {
            CacheExpiredTime = 60
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case GetAllTemporarilyRemovedDepartmentsStatusCode.INPUT_VALIDATION_FAIL:
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
                        Body = response.FoundTemporarilyRemovedDepartments.Select(department =>
                            new GetAllTemporarilyRemovedDepartmentsResponse.Department
                            {
                                DepartmentId = department.DepartmentId,
                                DepartmentName = department.DepartmentName,
                                DepartmentRemovedAt = TimeZoneInfo.ConvertTimeFromUtc(
                                    dateTime: department.DepartmentRemovedAt,
                                    destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(
                                        id: "SE Asia Standard Time")),
                                DepartmentRemovedBy = department.DepartmentRemovedBy
                            })
                    });
                }
        }
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
        RemoveDepartmentPermanentlyByDepartmentIdRequest request = new()
        {
            DepartmentId = departmentId,
            DepartmentRemovedBy = Guid.Parse("a2646515-306a-4667-9e41-2230391f61cd")
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case RemoveDepartmentPermanentlyByDepartmentIdStatusCode.INPUT_VALIDATION_FAIL:
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
            case RemoveDepartmentPermanentlyByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with Id = {departmentId} is not found."
                        }
                    });
                }

            // 400
            case RemoveDepartmentPermanentlyByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with Id = {departmentId} is not found in temporarily removed storage."
                        }
                    });
                }

            // 500
            case RemoveDepartmentPermanentlyByDepartmentIdStatusCode.DATABASE_OPERATION_FAIL:
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
        RestoreDepartmentByDepartmentIdRequest request = new()
        {
            DepartmentId = departmentId
        };

        var response = await _sender.Send(
            request: request,
            cancellationToken: cancellationToken);

        switch (response.StatusCode)
        {
            // 500
            case RestoreDepartmentByDepartmentIdStatusCode.INPUT_VALIDATION_FAIL:
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
            case RestoreDepartmentByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_FOUND:
                {
                    return NotFound(value: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_NOT_FOUND,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with Id = {departmentId} is not found."
                        }
                    });
                }

            // 400
            case RestoreDepartmentByDepartmentIdStatusCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED:
                {
                    return BadRequest(error: new CommonResponse
                    {
                        ApiReturnCode = DepartmentApiReturnCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED,
                        ErrorMessages = new List<string>(capacity: 1)
                        {
                            $"Department with Id = {departmentId} is not found in temporarily removed storage."
                        }
                    });
                }

            // 500
            case RestoreDepartmentByDepartmentIdStatusCode.DATABASE_OPERATION_FAIL:
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
