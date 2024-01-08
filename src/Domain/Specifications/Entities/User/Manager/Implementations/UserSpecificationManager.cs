using FuDeverWebApi.DataAccess.Specifications.Entites.User.Manager.Contratcs;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User.Manager.Implementations;

public sealed class UserSpecificationManager :
    IUserSpecificationManager
{
    // Backing fields.
    private IsUserNotSoftRemovedSpecification _isUserNotSoftRemovedSpecification;
    private IsUserSoftRemovedSpecification _isUserSoftRemovedSpecification;
    private SplitQueryOnUserSpecification _splitQueryOnUserSpecification;
    private NoTrackingOnUserSpecification _noTrackingOnUserSpecification;
    private SelectFieldsFromUserSpecification _selectFieldsFromUserSpecification;

    public IsUserNotSoftRemovedSpecification IsUserNotSoftRemovedSpecification
    {
        get
        {
            _isUserNotSoftRemovedSpecification ??= new();

            return _isUserNotSoftRemovedSpecification;
        }
    }

    public NoTrackingOnUserSpecification NoTrackingOnUserSpecification
    {
        get
        {
            _noTrackingOnUserSpecification ??= new();

            return _noTrackingOnUserSpecification;
        }
    }

    public SelectFieldsFromUserSpecification SelectFieldsFromUserSpecification
    {
        get
        {
            _selectFieldsFromUserSpecification ??= new();

            return _selectFieldsFromUserSpecification;
        }
    }

    public SplitQueryOnUserSpecification SplitQueryOnUserSpecification
    {
        get
        {
            _splitQueryOnUserSpecification ??= new();

            return _splitQueryOnUserSpecification;
        }
    }

    public IsUserSoftRemovedSpecification IsUserSoftRemovedSpecification
    {
        get
        {
            _isUserSoftRemovedSpecification ??= new();

            return _isUserSoftRemovedSpecification;
        }
    }

    public UserByIdSpecification UserByIdSpecification(
        Guid userId)
    {
        return new(userId: userId);
    }

    public UserByEmailSpecification UserByEmailSpecification(
        string email)
    {
        return new(email: email);
    }

    public UserByPhoneNumberSpecification UserByPhoneNumberSpecification(
        string phoneNumber)
    {
        return new(phoneNumber: phoneNumber);
    }

    public UserByUsernameSpecificaton UserByUsernameSpecificaton(
        string username)
    {
        return new(username: username);
    }

    public UserNotByIdSpecification UserNotByIdSpecification(
        Guid userId)
    {
        return new(userId: userId);
    }

    public UserByPositionIdSpecification UserByPositionIdSpecification(
        Guid positionId)
    {
        return new(positionId: positionId);
    }

    public UserByMajorIdSpecification UserByMajorIdSpecification(
        Guid majorId)
    {
        return new(majorId: majorId);
    }

    public UserByDepartmentIdSpecification UserByDepartmentIdSpecification(
        Guid departmentId)
    {
        return new(departmentId: departmentId);
    }
}
