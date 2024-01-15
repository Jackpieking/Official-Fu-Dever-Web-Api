namespace Application.ExternalFiles;

/// <summary>
///     Represent the handler for default user avatar url.
/// </summary>
public interface IDefaultAvatarUrlHandler
{
    /// <summary>
    ///     Get the default url for user avatar.
    /// </summary>
    string Get();
}
