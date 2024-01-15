using System;

namespace Application.Common;

/// <summary>
///     Represent set of constant.
/// </summary>
public static class CommonConstant
{
    public static class App
    {
        public static readonly Guid DEFAULT_ENTITY_ID_AS_GUID = Guid.NewGuid();

        public static readonly string DEFAULT_AVATAR_URL = "https://firebasestorage.googleapis.com/v0/b/comic-image-storage.appspot.com/o/blank-profile-picture-973460_1280.png?alt=media&token=2309abba-282c-4f81-846e-6336235103dc";
    }
}
