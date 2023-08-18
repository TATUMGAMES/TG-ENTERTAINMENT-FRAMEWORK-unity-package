using EntertainmentFramework;
using UnityEditor;
using UnityEngine;

namespace EntertainmentEditor
{
    public class EntertainmentFrameworkSettings : ScriptableObject
    {
        [SerializeField]
        [Tooltip(Constants.UnityAnalytics)]
        private bool unityAnalytics = false;

        [DrawIf("unityAnalytics", true, ComparisonType.Equals, DisablingType.ReadOnly)]
        [SerializeField]
        [Tooltip(Constants.SendUnityAnalytics)]
        private bool sendUnityAnalytics;

        [Tooltip(Constants.InitializeUnityAnalytics)]
        [DrawIf("unityAnalytics", true, ComparisonType.Equals, DisablingType.ReadOnly)]
        [SerializeField]
        private bool initalizeUnityAnalytics;

        /// <summary>
        /// Enable Unity Analytics for sending data.
        /// </summary>
        public bool EnableUnityAnalytics => unityAnalytics;

        /// <summary>
        /// Give automatically consent for collectiong data.
        /// </summary>
        public bool SendUnityAnalytics => sendUnityAnalytics;

        /// <summary>
        /// Enables auto Initalize of Unity Analtics.
        /// </summary>
        public bool EnableAutoInitalizeUnityAnalytics => initalizeUnityAnalytics;

        /// <summary>
        /// To prevent object creation of this class.
        /// </summary>
        private EntertainmentFrameworkSettings()
        {
        }

# if UNITY_EDITOR
        /// <summary>
        /// Editor-only function that Unity calls when the script is loaded or a value changes in the Inspector.
        /// </summary>
        private void OnValidate()
        {
            OnAnalyticsEventValueChange(unityAnalytics);
        }

        /// <summary>
        /// If there any change in the unityAnalytics value then this function will enable the function for sending Unity Analytics
        /// </summary>
        /// <param name="isunityAnalyticsEnable"></param>
        public static void OnAnalyticsEventValueChange(bool isunityAnalyticsEnable)
        {
            if (isunityAnalyticsEnable)
            {
                EditorUtilities.AddScriptingDefineSymbols(EditorConstants.UNITY_ANALYTICS_ENABLED, BuildTargetGroup.Android);
                EditorUtilities.AddScriptingDefineSymbols(EditorConstants.UNITY_ANALYTICS_ENABLED, BuildTargetGroup.iOS);
            }
            else
            {
                EditorUtilities.RemoveScriptingDefineSymbols(EditorConstants.UNITY_ANALYTICS_ENABLED, BuildTargetGroup.Android);
                EditorUtilities.RemoveScriptingDefineSymbols(EditorConstants.UNITY_ANALYTICS_ENABLED, BuildTargetGroup.iOS);
            }
        }
#endif
    }
}