namespace Application.Interfaces.ExternalFiles;

/// <summary>
///     Represent the handler for default avatar url.
/// </summary>
public interface IDefaultAvatarUrlHandler
{
    /// <summary>
    ///     Get the default url for avatar.
    /// </summary>
    string Get();
}
