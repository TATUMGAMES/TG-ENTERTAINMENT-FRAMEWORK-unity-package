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
    }
}