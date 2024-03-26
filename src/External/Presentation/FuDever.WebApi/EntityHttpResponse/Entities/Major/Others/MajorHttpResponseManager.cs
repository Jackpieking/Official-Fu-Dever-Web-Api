using FuDever.WebApi.EntityHttpResponse.Entities.Major.CreateMajor.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.GetAllMajors.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.GetAllMajorsByMajorName.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.GetAllTemporarilyRemovedMajors.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorPermanentlyByMajorId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RemoveMajorTemporarilyByMajorId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.RestoreMajorByMajorId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.UpdateMajorByMajorId.Others;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.Others;

/// <summary>
///     Handles all HTTP responses for major.
/// </summary>
internal sealed class MajorHttpResponseManager
{
    // Backing fields.
    private GetAllMajorsHttpResponseManager
        _getAllMajorsHttpResponseManager;
    private GetAllMajorsByMajorNameHttpResponseManager
        _getAllMajorsByMajorNameHttpResponseManager;
    private CreateMajorHttpResponseManager
        _createMajorHttpResponse;
    private GetAllTemporarilyRemovedMajorsHttpResponseManager
        _getAllTemporarilyRemovedMajorsHttpResponseManager;
    private RemoveMajorPermanentlyByMajorIdHttpResponseManager
        _removeMajorPermanentlyByMajorIdHttpResponseManager;
    private RemoveMajorTemporarilyByMajorIdHttpResponseManager
        _removeMajorTemporarilyByMajorIdHttpResponseManager;
    private UpdateMajorByMajorIdHttpResponseManager
        _updateMajorByMajorIdHttpResponseManager;
    private RestoreMajorByMajorIdHttpResponseManager
        _restoreMajorByMajorIdHttpResponseManager;

    internal GetAllMajorsHttpResponseManager GetAllMajors
    {
        get
        {
            _getAllMajorsHttpResponseManager ??= new();

            return _getAllMajorsHttpResponseManager;
        }
    }

    internal GetAllMajorsByMajorNameHttpResponseManager GetAllMajorsByMajorName
    {
        get
        {
            _getAllMajorsByMajorNameHttpResponseManager ??= new();

            return _getAllMajorsByMajorNameHttpResponseManager;
        }
    }

    internal CreateMajorHttpResponseManager CreateMajor
    {
        get
        {
            _createMajorHttpResponse ??= new();

            return _createMajorHttpResponse;
        }
    }

    internal GetAllTemporarilyRemovedMajorsHttpResponseManager GetAllTemporarilyRemovedMajors
    {
        get
        {
            _getAllTemporarilyRemovedMajorsHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedMajorsHttpResponseManager;
        }
    }

    internal RemoveMajorPermanentlyByMajorIdHttpResponseManager RemoveMajorPermanentlyByMajorId
    {
        get
        {
            _removeMajorPermanentlyByMajorIdHttpResponseManager ??= new();

            return _removeMajorPermanentlyByMajorIdHttpResponseManager;
        }
    }

    internal RemoveMajorTemporarilyByMajorIdHttpResponseManager RemoveMajorTemporarilyByMajorId
    {
        get
        {
            _removeMajorTemporarilyByMajorIdHttpResponseManager ??= new();

            return _removeMajorTemporarilyByMajorIdHttpResponseManager;
        }
    }

    internal UpdateMajorByMajorIdHttpResponseManager UpdateMajorByMajorId
    {
        get
        {
            _updateMajorByMajorIdHttpResponseManager ??= new();

            return _updateMajorByMajorIdHttpResponseManager;
        }
    }

    internal RestoreMajorByMajorIdHttpResponseManager RestoreMajorByMajorId
    {
        get
        {
            _restoreMajorByMajorIdHttpResponseManager ??= new();

            return _restoreMajorByMajorIdHttpResponseManager;
        }
    }
}
