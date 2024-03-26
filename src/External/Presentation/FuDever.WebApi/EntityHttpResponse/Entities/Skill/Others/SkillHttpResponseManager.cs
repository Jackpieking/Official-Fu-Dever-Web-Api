using FuDever.WebApi.EntityHttpResponse.Entities.Skill.CreateSkill.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllSkills.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllSkillsBySkillName.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllTemporarilyRemovedSkills.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillPermanentlyBySkillId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillTemporarilyBySkillId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.RestoreSkillBySkillId.Others;
using FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId.Others;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.Others;

/// <summary>
///     Handles all HTTP responses for skill.
/// </summary>
internal sealed class SkillHttpResponseManager
{
    // Backing fields.
    private GetAllSkillsHttpResponseManager
        _getAllSkillsHttpResponseManager;
    private GetAllSkillsBySkillNameHttpResponseManager
        _getAllSkillsBySkillNameHttpResponseManager;
    private CreateSkillHttpResponseManager
        _createSkillHttpResponseManager;
    private GetAllTemporarilyRemovedSkillsHttpResponseManager
        _getAllTemporarilyRemovedSkillsHttpResponseManager;
    private RemoveSkillPermanentlyBySkillIdHttpResponseManager
        _removeSkillPermanentlyBySkillIdHttpResponseManager;
    private RemoveSkillTemporarilyBySkillIdHttpResponseManager
        _removeSkillTemporarilyBySkillIdHttpResponseManager;
    private UpdateSkillBySkillIdHttpResponseManager
        _updateSkillBySkillIdHttpResponseManager;
    private RestoreSkillBySkillIdHttpResponseManager
        _restoreSkillBySkillIdHttpResponseManager;

    internal GetAllSkillsHttpResponseManager GetAllSkills
    {
        get
        {
            _getAllSkillsHttpResponseManager ??= new();

            return _getAllSkillsHttpResponseManager;
        }
    }

    internal GetAllSkillsBySkillNameHttpResponseManager GetAllSkillsBySkillName
    {
        get
        {
            _getAllSkillsBySkillNameHttpResponseManager ??= new();

            return _getAllSkillsBySkillNameHttpResponseManager;
        }
    }

    internal CreateSkillHttpResponseManager CreateSkill
    {
        get
        {
            _createSkillHttpResponseManager ??= new();

            return _createSkillHttpResponseManager;
        }
    }

    internal GetAllTemporarilyRemovedSkillsHttpResponseManager GetAllTemporarilyRemovedSkills
    {
        get
        {
            _getAllTemporarilyRemovedSkillsHttpResponseManager ??= new();

            return _getAllTemporarilyRemovedSkillsHttpResponseManager;
        }
    }

    internal RemoveSkillPermanentlyBySkillIdHttpResponseManager RemoveSkillPermanentlyBySkillId
    {
        get
        {
            _removeSkillPermanentlyBySkillIdHttpResponseManager ??= new();

            return _removeSkillPermanentlyBySkillIdHttpResponseManager;
        }
    }

    internal RemoveSkillTemporarilyBySkillIdHttpResponseManager RemoveSkillTemporarilyBySkillId
    {
        get
        {
            _removeSkillTemporarilyBySkillIdHttpResponseManager ??= new();

            return _removeSkillTemporarilyBySkillIdHttpResponseManager;
        }
    }

    internal UpdateSkillBySkillIdHttpResponseManager UpdateSkillBySkillId
    {
        get
        {
            _updateSkillBySkillIdHttpResponseManager ??= new();

            return _updateSkillBySkillIdHttpResponseManager;
        }
    }

    internal RestoreSkillBySkillIdHttpResponseManager RestoreSkillBySkillId
    {
        get
        {
            _restoreSkillBySkillIdHttpResponseManager ??= new();

            return _restoreSkillBySkillIdHttpResponseManager;
        }
    }
}
