using Application.Features.Department.CreateDepartment.Middlewares;
using MediatR;

namespace Application.Features.Department.CreateDepartment;

/// <summary>
///     Create department request.
/// </summary>
public sealed class CreateDepartmentRequest :
    IRequest<CreateDepartmentResponse>,
    ICreateDepartmentMiddleware
{
    public string NewDepartmentName { get; init; }
}
