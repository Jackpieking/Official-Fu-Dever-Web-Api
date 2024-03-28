using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.CreateHobby.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.GetAllHobbies.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.GetAllHobbiesByHobbyName.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.GetAllTemporarilyRemovedHobbies.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RemoveHobbyPermanentlyByHobbyId.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RemoveHobbyTemporarilyByHobbyId.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.RestoreHobbyByHobbyId.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.UpdateHobbyByHobbyId.Others;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.Others;

/// <summary>
///     Handles all HTTP responses for hobby.
/// </summary>
internal sealed class HobbyHttpResponseManager
{
    // Backing fields.
    private GetAllHobbiesHttpResponseManager
        _getAllHobbiesHttpResponseManager;
    private GetAllHobbiesByHobbyNameHttpResponseManager
        _getAllHobbiesByHobbyNameHttpResponseManager;
    private CreateHobbyHttpResponseManager
        _createHobbyHttpResponseManager;
    private GetAllTemporarilyRemovedHobbiesHttpResponseManager
        _getAllTemporarilyRemovedHobbiesHttpResponseManager;
    private RemoveHobbyPermanentlyByHobbyIdHttpResponseManager
        _removeHobbyPermanentlyByHobbyIdHttpResponseManager;
    private RemoveHobbyTemporarilyByHobbyIdHttpResponseManager
        _removeHobbyTemporarilyByHobbyIdHttpResponseManager;
    private UpdateHobbyByHobbyIdHttpResponseManager
        _updateHobbyByHobbyIdHttpResponseManager;
    private RestoreHobbyByHobbyIdHttpResponseManager
        _restoreHobbyByHobbyIdHttpResponseManager;

    internal GetAllHobbiesHttpResponseManager GetAllHobbies
    {
        get
        {
            _getAllHobbiesHttpResponseManager ??= new();

            return _getAllHobbiesHttpResponseManager;
        }
    }

    internal GetAllHobbiesByHobbyNameHttpResponseManager GetAllHobbiesByHobbyName
    {
        get
        {
            _getAllHobbiesByHobbyNameHttpResponseManager ??= new();

            return _getAllHobbiesByHobbyNameHttpResponseManager;
        }
    }

    internal CreateHobbyHttpResponseManager CreateHobby
    {
        get
        {
            _createHobbyHttpResponseManager ??= new();

            return _createHobbyHttpResponseManager;
        }
    }

    internal GetAllTemporarilyRemovedHobbiesHttpResponseManager GetAllTemporarilyRemovedHobbies
    {
        get
        {
            _getAllTemporarilyRemovedHobbiesHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedHobbiesHttpResponseManager;
        }
    }

    internal RemoveHobbyPermanentlyByHobbyIdHttpResponseManager RemoveHobbyPermanentlyByHobbyId
    {
        get
        {
            _removeHobbyPermanentlyByHobbyIdHttpResponseManager ??= new();

            return _removeHobbyPermanentlyByHobbyIdHttpResponseManager;
        }
    }

    internal RemoveHobbyTemporarilyByHobbyIdHttpResponseManager RemoveHobbyTemporarilyByHobbyId
    {
        get
        {
            _removeHobbyTemporarilyByHobbyIdHttpResponseManager ??= new();

            return _removeHobbyTemporarilyByHobbyIdHttpResponseManager;
        }
    }

    internal UpdateHobbyByHobbyIdHttpResponseManager UpdateHobbyByHobbyId
    {
        get
        {
            _updateHobbyByHobbyIdHttpResponseManager ??= new();

            return _updateHobbyByHobbyIdHttpResponseManager;
        }
    }

    internal RestoreHobbyByHobbyIdHttpResponseManager RestoreHobbyByHobbyId
    {
        get
        {
            _restoreHobbyByHobbyIdHttpResponseManager ??= new();

            return _restoreHobbyByHobbyIdHttpResponseManager;
        }
    }
}
