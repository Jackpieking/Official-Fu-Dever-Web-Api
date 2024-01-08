using Application.Interfaces.ExternalFiles;

namespace OtherResources.ExternalFiles;

/// <summary>
///     Implementation of default avatar url handler.
/// </summary>
public sealed class DefaultAvatarUrlHandler : IDefaultAvatarUrlHandler
{
    /// <summary>
    ///     Return the default avatar url.
    /// </summary>
    /// <returns>
    ///     Avatar url.
    /// </returns>
    public string Get()
    {
        const string DefaultAvatarUrl = "https://firebasestorage.googleapis.com/v0/b/comic-image-storage.appspot.com/o/blank-profile-picture-973460_1280.png?alt=media&token=2309abba-282c-4f81-846e-6336235103dc";

        return DefaultAvatarUrl;
    }
}
