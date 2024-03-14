using FuDever.Application.Interfaces.ExternalFiles;

namespace FuDever.Firebase.Image;

/// <summary>
///     Implementation of default avatar url handler.
/// </summary>
internal sealed class DefaultUserAvatarAsUrlFirebaseSourceHandler : IDefaultUserAvatarAsUrlHandler
{
    private const string DefaultUserAvatarUrl = "https://firebasestorage.googleapis.com/v0/b/comic-image-storage.appspot.com/o/blank-profile-picture-973460_1280.png?alt=media&token=2309abba-282c-4f81-846e-6336235103dc";

    public string Get()
    {
        return DefaultUserAvatarUrl;
    }
}
