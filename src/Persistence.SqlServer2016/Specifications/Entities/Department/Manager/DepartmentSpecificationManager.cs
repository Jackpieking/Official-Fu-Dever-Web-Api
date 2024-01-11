using Domain.Specifications.Entities.Department;
using Domain.Specifications.Entities.Department.Manager;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Department.Manager;

internal sealed class DepartmentSpecificationManager : IDepartmentSpecificationManager
{
    // Backing fields.
    private IDepartmentAsNoTrackingSpecification _departmentAsNoTrackingSpecification;
    private IDepartmentNotTemporarilyRemovedSpecification _departmentNotTemporarilyRemovedSpecification;
    private IDepartmentTemporarilyRemovedSpecification _departmentTemporarilyRemovedSpecification;
    private ISelectFieldsFromDepartmentSpecification _selectFieldsFromDepartmentSpecification;

    public IDepartmentAsNoTrackingSpecification DepartmentAsNoTrackingSpecification
    {
        get
        {
            _departmentAsNoTrackingSpecification ??= new DepartmentAsNoTrackingSpecification();

            return _departmentAsNoTrackingSpecification;
        }
    }

    public IDepartmentNotTemporarilyRemovedSpecification DepartmentNotTemporarilyRemovedSpecification
    {
        get
        {
            _departmentNotTemporarilyRemovedSpecification ??= new DepartmentNotTemporarilyRemovedSpecification();

            return _departmentNotTemporarilyRemovedSpecification;
        }
    }

    public IDepartmentTemporarilyRemovedSpecification DepartmentTemporarilyRemovedSpecification
    {
        get
        {
            _departmentTemporarilyRemovedSpecification ??= new DepartmentTemporarilyRemovedSpecification();

            return _departmentTemporarilyRemovedSpecification;
        }
    }

    public ISelectFieldsFromDepartmentSpecification SelectFieldsFromDepartmentSpecification
    {
        get
        {
            _selectFieldsFromDepartmentSpecification ??= new SelectFieldsFromDepartmentSpecification();

            return _selectFieldsFromDepartmentSpecification;
        }
    }

    public IDepartmentByIdSpecification DepartmentByIdSpecification(Guid departmentId)
    {
        return new DepartmentByIdSpecification(departmentId: departmentId);
    }

    public IDepartmentByNameSpecification DepartmentByNameSpecification(
        string departmentName,
        bool isCaseSensitive)
    {
        return new DepartmentByNameSpecification(
            departmentName: departmentName,
            isCaseSensitive: isCaseSensitive);
    }
}
