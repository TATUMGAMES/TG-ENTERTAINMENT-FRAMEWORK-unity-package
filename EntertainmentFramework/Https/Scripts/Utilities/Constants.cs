using UnityEngine;

namespace EntertainmentFramework
{
    internal sealed class Constants
    {
        internal const string WebRequestManager = "Web Request Manager";

        internal const string ContentTypeKey = "Content-Type";

        internal const string AcceptTypeKey = "Accept";

        internal const string JsonWebContent = "application/json";

        internal const string NO_CONNECTION_ERROR = "no_connection";

        internal const long HttpOk = 200;

        internal const long HttpCreated = 201;

        internal const string EntertainmentInternalLogger = "Entertainment";

        #region Mikros/Analytics

        internal static readonly string DeviceID = SystemInfo.deviceUniqueIdentifier;

        internal const string Production = "prod";
        internal const string Qa = "qa";
        internal const string Development = "dev";

        internal const string ValueRequiredMessage = "This value is required";

        #endregion Mikros/Analytics

        #region Wrapper Settings

        internal const string UnityAnalytics = "Enable Unity Analytics";
        internal const string SendUnityAnalytics = "Constent to send data to  Unity Analytics";
        internal const string InitializeUnityAnalytics = "Auto Initialize Unity Analytics";
        internal const string MikrosSettingsAssetName = "Entertainment Settings";
        internal const string ExceptionMessage = "detailMessage";
        internal const string Exception= "exception";

        #endregion Wrapper Settings  

        #region SignIn/SingUp Validation
        internal const string USERNAME_MINIMUM_CHARACTER_LIMIT = "Username must have a minimum of 6 characters.";
        internal const string USERNAME_MAXIMUM_CHARACTER_LIMIT = "Username must have a maximum of 30 characters.";
        internal const string USERNAME_SPACE_VALIDATION = "Username cannot contain spaces.";
        internal const string USERNAME_SPECIAL_CHARACTER_VALIDATION = "Special characters are not allowed in the username.";
        internal const string USERNAME_STARTING_CHARACTER_VALIDATION = "Username must start with an alphabetic character.";

        public const string EMAIL_VALIDATION = "Enter this value in the format: email@example.com";

        public const string PASSWORD_SPACE_VALIDATION = "The password you submitted contains spaces.";
        public const string PASSWORD_MINIMUM_CHARACTER_VALIDATION = "Password must have minimum 6 characters.";
        #endregion SignIn/SingUp Validation

        #region Sound Data

        public const string SOUND_ON_OFF = "SOUND_ON_OFF";// 0= off 1= on
        public const string MUSIC_ON_OFF = "MUSIC_ON_OFF";
        public const string SOUND_VOLUME = "SOUND_VOLUME";
        public const string MUSIC_VOLUME = "MUSIC_VOLUME";
        #endregion SOund Data
    }
}