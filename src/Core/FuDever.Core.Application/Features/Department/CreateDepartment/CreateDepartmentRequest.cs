using FuDever.Application.Features.Department.CreateDepartment.Middlewares;
using MediatR;

namespace FuDever.Application.Features.Department.CreateDepartment;

/// <summary>
///     Create department request.
/// </summary>
public sealed class CreateDepartmentRequest :
    IRequest<CreateDepartmentResponse>,
    ICreateDepartmentMiddleware
{
    public string NewDepartmentName { get; init; }
}
