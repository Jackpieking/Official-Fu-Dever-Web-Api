using Domain.Specifications.Entities.Major;
using Domain.Specifications.Entities.Major.Manager;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.Major.Manager;

/// <summary>
///     Represent implementation of major specification manager.
/// </summary>
internal sealed class MajorSpecificationManager : IMajorSpecificationManager
{
    // Backing fields
    private IMajorAsNoTrackingSpecification _majorAsNoTrackingSpecification;
    private IMajorNotTemporarilyRemovedSpecification _majorNotTemporarilyRemovedSpecification;
    private IMajorTemporarilyRemovedSpecification _majorTemporarilyRemovedSpecification;
    private ISelectFieldsFromMajorSpecification _selectFieldsFromMajorSpecification;

    public IMajorAsNoTrackingSpecification MajorAsNoTrackingSpecification
    {
        get
        {
            _majorAsNoTrackingSpecification ??= new MajorAsNoTrackingSpecification();

            return _majorAsNoTrackingSpecification;
        }
    }

    public IMajorNotTemporarilyRemovedSpecification MajorNotTemporarilyRemovedSpecification
    {
        get
        {
            _majorNotTemporarilyRemovedSpecification ??= new MajorNotTemporarilyRemovedSpecification();

            return _majorNotTemporarilyRemovedSpecification;
        }
    }

    public IMajorTemporarilyRemovedSpecification MajorTemporarilyRemovedSpecification
    {
        get
        {
            _majorTemporarilyRemovedSpecification ??= new MajorTemporarilyRemovedSpecification();

            return _majorTemporarilyRemovedSpecification;
        }
    }

    public ISelectFieldsFromMajorSpecification SelectFieldsFromMajorSpecification
    {
        get
        {
            _selectFieldsFromMajorSpecification ??= new SelectFieldsFromMajorSpecification();

            return _selectFieldsFromMajorSpecification;
        }
    }

    public IMajorByIdSpecification MajorByIdSpecification(Guid majorId)
    {
        return new MajorByIdSpecification(majorId: majorId);
    }

    public IMajorByNameSpecification MajorByNameSpecification(
        string majorName,
        bool isCaseSensitive)
    {
        return new MajorByNameSpecification(
            majorName: majorName,
            isCaseSensitive: isCaseSensitive);
    }
}
