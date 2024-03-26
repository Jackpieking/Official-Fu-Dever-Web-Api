using FuDever.WebApi.EntityHttpResponse.Entities.Platform.CreatePlatform.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatforms.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllPlatformsByPlatformName.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.GetAllTemporarilyRemovedPlatforms.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformPermanentlyByPlatformId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.RemovePlatformTemporarilyByPlatformId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.RestorePlatformByPlatformId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Platform.UpdatePlatformByPlatformId.Others;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Platform.Others;

/// <summary>
///     Handles all HTTP responses for platform.
/// </summary>
internal sealed class PlatformHttpResponseManager
{
    // Backing fields.
    private GetAllPlatformsHttpResponseManager
        _getAllPlatformsHttpResponseManager;
    private GetAllPlatformsByPlatformNameHttpResponseManager
        _getAllPlatformsByPlatformNameHttpResponseManager;
    private CreatePlatformHttpResponseManager
        _createPlatformHttpResponseManager;
    private GetAllTemporarilyRemovedPlatformsHttpResponseManager
        _getAllTemporarilyRemovedPlatformsHttpResponseManager;
    private RemovePlatformPermanentlyByPlatformIdHttpResponseManager
        _removePlatformPermanentlyByPlatformIdHttpResponseManager;
    private RemovePlatformTemporarilyByPlatformIdHttpResponseManager
        _removePlatformTemporarilyByPlatformIdHttpResponseManager;
    private UpdatePlatformByPlatformIdHttpResponseManager
        _updatePlatformByPlatformIdHttpResponseManager;
    private RestorePlatformByPlatformIdHttpResponseManager
        _restorePlatformByPlatformIdHttpResponseManager;

    internal GetAllPlatformsHttpResponseManager GetAllPlatforms
    {
        get
        {
            _getAllPlatformsHttpResponseManager ??= new();

            return _getAllPlatformsHttpResponseManager;
        }
    }

    internal GetAllPlatformsByPlatformNameHttpResponseManager GetAllPlatformsByPlatformName
    {
        get
        {
            _getAllPlatformsByPlatformNameHttpResponseManager ??= new();

            return _getAllPlatformsByPlatformNameHttpResponseManager;
        }
    }

    internal CreatePlatformHttpResponseManager CreatePlatform
    {
        get
        {
            _createPlatformHttpResponseManager ??= new();

            return _createPlatformHttpResponseManager;
        }
    }

    internal GetAllTemporarilyRemovedPlatformsHttpResponseManager GetAllTemporarilyRemovedPlatforms
    {
        get
        {
            _getAllTemporarilyRemovedPlatformsHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedPlatformsHttpResponseManager;
        }
    }

    internal RemovePlatformPermanentlyByPlatformIdHttpResponseManager RemovePlatformPermanentlyByPlatformId
    {
        get
        {
            _removePlatformPermanentlyByPlatformIdHttpResponseManager ??= new();

            return _removePlatformPermanentlyByPlatformIdHttpResponseManager;
        }
    }

    internal RemovePlatformTemporarilyByPlatformIdHttpResponseManager RemovePlatformTemporarilyByPlatformId
    {
        get
        {
            _removePlatformTemporarilyByPlatformIdHttpResponseManager ??= new();

            return _removePlatformTemporarilyByPlatformIdHttpResponseManager;
        }
    }

    internal UpdatePlatformByPlatformIdHttpResponseManager UpdatePlatformByPlatformId
    {
        get
        {
            _updatePlatformByPlatformIdHttpResponseManager ??= new();

            return _updatePlatformByPlatformIdHttpResponseManager;
        }
    }

    internal RestorePlatformByPlatformIdHttpResponseManager RestorePlatformByPlatformId
    {
        get
        {
            _restorePlatformByPlatformIdHttpResponseManager ??= new();

            return _restorePlatformByPlatformIdHttpResponseManager;
        }
    }
}
