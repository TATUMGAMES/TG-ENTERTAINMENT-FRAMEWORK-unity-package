#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace EntertainmentEditor
{
    /// <summary>
    /// Menu Editor class for Entertainment Settings.
    /// </summary>
    internal class EntertainmentSettingsMenuEditor
    {
        private readonly static string wrapperSettingsFileName = EditorConstants.WrapperSettingsFileName;
        private readonly static string extensionName = EditorConstants.WrapperSettingsExtensionName;
        private readonly static string wrapperSettingsAssetPath = EditorConstants.WrapperSettingsAssetPath;

        /// <summary>
        /// Generate or returns a wrapper Settings scriptable object on first import of wrapper package.
        /// </summary>
        /// <returns>wrapperSettings instance.</returns>
        [InitializeOnLoadMethod]
        private static EntertainmentFrameworkSettings InitializWrapperSettingsAsset()
        {
            EntertainmentFrameworkSettings wrapperSettingsAsset = GetWrapperSettingsAsset();
            if (wrapperSettingsAsset == null)
            {
                //Debug.LogWarning(EditorConstants.wrapperSettingsWarningMessage);
                return CreateWrapperSettingsAsset();
            }
            else
            {
                return wrapperSettingsAsset;
            }
        }

        /// <summary>
        /// Returns wrapper Settings asset. (returns null, if not found)
        /// </summary>
        /// <returns>wrapperSettings instance.</returns>
        private static EntertainmentFrameworkSettings GetWrapperSettingsAsset()
        {
            EntertainmentFrameworkSettings wrapperSettingsObject = Resources.Load<EntertainmentFrameworkSettings>(wrapperSettingsFileName);
            return wrapperSettingsObject;
        }

        /// <summary>
        /// Create a new wrapper Settings asset file.
        /// </summary>
        /// <returns>wrapperSettings instance.</returns>
        private static EntertainmentFrameworkSettings CreateWrapperSettingsAsset()
        {
            EntertainmentFrameworkSettings wrapperSettingsAsset = ScriptableObject.CreateInstance<EntertainmentFrameworkSettings>();
            string[] splittedPath = wrapperSettingsAssetPath.Split('/');
            if (!AssetDatabase.IsValidFolder(wrapperSettingsAssetPath))
                AssetDatabase.CreateFolder(splittedPath[0], splittedPath[1]);
            AssetDatabase.CreateAsset(wrapperSettingsAsset, wrapperSettingsAssetPath + "/" + wrapperSettingsFileName + extensionName);
            AssetDatabase.SaveAssets();
            //FocuswrapperSettingsAsset(wrapperSettingsAsset);
            return wrapperSettingsAsset;
        }

        /// <summary>
        /// Select and focus wrapper Settings asset in inspector.
        /// </summary>
        [MenuItem(EditorConstants.MenuItemPath, false, 0)]
        private static void EditWrapperSettings()
        {
            EntertainmentFrameworkSettings wrapperSettingsObject = InitializWrapperSettingsAsset();
            FocusWrapperSettingsAsset(wrapperSettingsObject);
        }

        /// <summary>
        /// Focus an object in project to show in inspector.
        /// </summary>
        /// <param name="wrapperSettingsObject">wrapperSettings instance as Object.</param>
        private static void FocusWrapperSettingsAsset(Object wrapperSettingsObject)
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = wrapperSettingsObject;
        }        
    }
}
#endif