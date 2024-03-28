using FuDever.WebApi.HttpResponseMapper.Entities.Position.CreatePosition.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.GetAllPositions.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.GetAllPositionsByPositionName.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.GetAllTemporarilyRemovedPositions.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.RemovePositionPermanentlyByPositionId.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.RemovePositionTemporarilyByPositionId.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.RestorePositionByPositionId.Others;
using FuDever.WebApi.HttpResponseMapper.Entities.Position.UpdatePositionByPositionId.Others;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Position.Others;

/// <summary>
///     Handles all HTTP responses for position.
/// </summary>
internal sealed class PositionHttpResponseManager
{
    // Backing fields.
    private GetAllPositionsHttpResponseManager
        _getAllPositionsHttpResponseManager;
    private GetAllPositionsByPositionNameHttpResponseManager
        _getAllPositionsByPositionNameHttpResponseManager;
    private CreatePositionHttpResponseManager
        _createPositionHttpResponseManager;
    private GetAllTemporarilyRemovedPositionsHttpResponseManager
        _getAllTemporarilyRemovedPositionsHttpResponseManager;
    private RemovePositionPermanentlyByPositionIdHttpResponseManager
        _removePositionPermanentlyByPositionIdHttpResponseManager;
    private RemovePositionTemporarilyByPositionIdHttpResponseManager
        _removePositionTemporarilyByPositionIdHttpResponseManager;
    private UpdatePositionByPositionIdHttpResponseManager
        _updatePositionByPositionIdHttpResponseManager;
    private RestorePositionByPositionIdHttpResponseManager
        _restorePositionByPositionIdHttpResponseManager;

    internal GetAllPositionsHttpResponseManager GetAllPositions
    {
        get
        {
            _getAllPositionsHttpResponseManager ??= new();

            return _getAllPositionsHttpResponseManager;
        }
    }

    internal GetAllPositionsByPositionNameHttpResponseManager GetAllPositionsByPositionName
    {
        get
        {
            _getAllPositionsByPositionNameHttpResponseManager ??= new();

            return _getAllPositionsByPositionNameHttpResponseManager;
        }
    }

    internal CreatePositionHttpResponseManager CreatePosition
    {
        get
        {
            _createPositionHttpResponseManager ??= new();

            return _createPositionHttpResponseManager;
        }
    }

    internal GetAllTemporarilyRemovedPositionsHttpResponseManager GetAllTemporarilyRemovedPositions
    {
        get
        {
            _getAllTemporarilyRemovedPositionsHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedPositionsHttpResponseManager;
        }
    }

    internal RemovePositionPermanentlyByPositionIdHttpResponseManager RemovePositionPermanentlyByPositionId
    {
        get
        {
            _removePositionPermanentlyByPositionIdHttpResponseManager ??= new();

            return _removePositionPermanentlyByPositionIdHttpResponseManager;
        }
    }

    internal RemovePositionTemporarilyByPositionIdHttpResponseManager RemovePositionTemporarilyByPositionId
    {
        get
        {
            _removePositionTemporarilyByPositionIdHttpResponseManager ??= new();

            return _removePositionTemporarilyByPositionIdHttpResponseManager;
        }
    }

    internal UpdatePositionByPositionIdHttpResponseManager UpdatePositionByPositionId
    {
        get
        {
            _updatePositionByPositionIdHttpResponseManager ??= new();

            return _updatePositionByPositionIdHttpResponseManager;
        }
    }

    internal RestorePositionByPositionIdHttpResponseManager RestorePositionByPositionId
    {
        get
        {
            _restorePositionByPositionIdHttpResponseManager ??= new();

            return _restorePositionByPositionIdHttpResponseManager;
        }
    }
}
