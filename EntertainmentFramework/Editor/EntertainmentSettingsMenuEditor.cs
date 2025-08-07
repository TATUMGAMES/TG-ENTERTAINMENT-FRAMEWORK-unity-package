#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using EntertainmentFramework.AudioHandler;

namespace EntertainmentEditor
{
    /// <summary>
    /// Menu Editor class for Entertainment Settings.
    /// </summary>
    internal class EntertainmentSettingsMenuEditor
    {
        private readonly static string soundDataFileName = EditorConstants.SoundDataSettingFileName;
        private readonly static string extensionName = EditorConstants.WrapperSettingsExtensionName;
        private readonly static string wrapperSettingsFileName = EditorConstants.WrapperSettingsFileName;
        private readonly static string buildSettingsFileName = EditorConstants.BuildVersionSettingFileName;
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
                return CreateWrapperSettingsAsset();
            }
            else
            {
                return wrapperSettingsAsset;
            }
        }
        /// <summary>
        /// Generate a Build Version Config scriptable object on first import of wrapper package.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void InitializBuildVersionSettingsAsset()
        {
            BuildVersionConfig buildConfigObject = Resources.Load<BuildVersionConfig>(buildSettingsFileName);
            if (buildConfigObject == null)
            {
                CreateBuildSettingsAsset();
            }            
        }

        /// <summary>
        /// Generate Sound Data scriptable object on first import of wrapper package.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void InitializeSoundDataSettingsAsset()
        {
            SoundData soundDataObject = Resources.Load<SoundData>(soundDataFileName);
            if (soundDataObject == null)
            {
                CreateSoundDataAsset();
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
            return wrapperSettingsAsset;

        }

        /// <summary>
        /// Create a new Sound Data asset file.
        /// </summary>
        /// <returns>wrapperSettings instance.</returns>
        [MenuItem(EditorConstants.SoundDataAssetPath, false, 1)]
        private static void CreateSoundDataAsset()
        {
            SoundData soundDataObject = Resources.Load<SoundData>(soundDataFileName);
            if (soundDataObject == null)
            {
                SoundData soundAssets = ScriptableObject.CreateInstance<SoundData>();
                string[] splittedPath = wrapperSettingsAssetPath.Split('/');
                if (!AssetDatabase.IsValidFolder(wrapperSettingsAssetPath))
                    AssetDatabase.CreateFolder(splittedPath[0], splittedPath[1]);
                AssetDatabase.CreateAsset(soundAssets, wrapperSettingsAssetPath + "/" + soundDataFileName + extensionName);
                AssetDatabase.SaveAssets();
                FocusWrapperSettingsAsset(soundAssets);
            }
            else
            {
                FocusWrapperSettingsAsset(soundDataObject);

            }

        }

        /// <summary>
        /// Select and focus build Settings asset in inspector.
        /// </summary>
        [MenuItem(EditorConstants.BuildAssetPath, false, 0)]
        private static void CreateBuildSettingsAsset()
        {
            BuildVersionConfig buildConfigObject = Resources.Load<BuildVersionConfig>(buildSettingsFileName);
            if (buildConfigObject == null)
            {
                BuildVersionConfig buildAsset = ScriptableObject.CreateInstance<BuildVersionConfig>();
                string[] splittedPath = wrapperSettingsAssetPath.Split('/');
                if (!AssetDatabase.IsValidFolder(wrapperSettingsAssetPath))
                    AssetDatabase.CreateFolder(splittedPath[0], splittedPath[1]);
                AssetDatabase.CreateAsset(buildAsset, wrapperSettingsAssetPath + "/" + buildSettingsFileName + extensionName);
                AssetDatabase.SaveAssets();
                FocusWrapperSettingsAsset(buildAsset);
            }
            else
            {
                FocusWrapperSettingsAsset(buildConfigObject);
            }
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