#if UNITY_EDITOR

using EntertainmentEditor;
using EntertainmentFramework.InternalLoggers;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

#endif

namespace MikrosEditor
{
    /// <summary>
    /// Handles loading of the Mikros SDK from git repo via UPM.
    /// </summary>
    internal static class MikrosPackageLoader
    {
#if UNITY_EDITOR
        private const string mikrosDependencyKey = "com.tatumgames.mikros";
        private const string unityAnalyticsDependencyKey = "com.unity.services.analytics";

        private const string packjsonPathLocation = "Packages/packages-lock.json";
        private const string mikrosDependencyValue = "https://github.com/TATUMGAMES/TG-MIKROS-SDK-unity-package.git#1.3.1-release";

        private static ListRequest listRequest;
        private static AddRequest addRequest;

        /// <summary>
        /// Get PackagesList.
        /// Checks all existing packages and installs Mikros Unity SDK if not present in project.
        /// Note: Runs automatically very first time the script is added to project.
        /// </summary>
        [InitializeOnLoadMethod]
        private static void GetPackagesList()
        {
            listRequest = Client.List();    // List packages installed for the project
            EditorApplication.update += PackageListProgress;
            IsPackageInstalled(unityAnalyticsDependencyKey);
        }

        /// <summary>
        /// Add a new package to project.
        /// </summary>
        private static void AddPackage()
        {
            InternalLogger.Log("MIKROS SDK package not found. Initiating installation..");
            addRequest = Client.Add(string.Join("@", mikrosDependencyKey, mikrosDependencyValue));
            EditorApplication.update += PackageAddProgress;
        }

        /// <summary>
        /// Package List Progress.
        /// Event triggered upon successfully getting all installed packages.
        /// Scan packages for Mikros.
        /// If Mikros is not found, add Mikros to packages.
        /// </summary>
        private static void PackageListProgress()
        {
            if (listRequest.IsCompleted)
            {
                if (listRequest.Status == StatusCode.Success)
                {
                    bool isMikrosPresent = false;
                    foreach (var package in listRequest.Result)
                    {
                        if (package.packageId.Contains(mikrosDependencyKey))
                        {
                            EditorUtilities.AddScriptingDefineSymbols(EditorConstants.MIKROS_ADDED, BuildTargetGroup.Android);
                            EditorUtilities.AddScriptingDefineSymbols(EditorConstants.MIKROS_ADDED, BuildTargetGroup.iOS);

                            isMikrosPresent = true;
                            break;
                        }
                    }
                    if (!isMikrosPresent)
                    {
                        AddPackage();
                    }
                }
                else if (listRequest.Status >= StatusCode.Failure)
                {
                    Debug.Log(listRequest.Error.message);
                }

                EditorApplication.update -= PackageListProgress;
            }
        }

        /// <summary>
        /// Package Add Progress.
        /// Event triggered after successfully installing Mikros package to project.
        /// </summary>
        private static void PackageAddProgress()
        {
            if (addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    Client.Resolve();
                    InternalLogger.Log("MIKROS SDK package installed successfully.");
                }
                else if (addRequest.Status >= StatusCode.Failure)
                {
                    InternalLogger.LogError(addRequest.Error.message);
                }
                EditorApplication.update -= PackageAddProgress;
            }
        }

        public static bool IsPackageInstalled(string checkRequiredPackage)
        {
            if (!File.Exists(packjsonPathLocation))
                return false;

            string jsonText = File.ReadAllText(packjsonPathLocation);
            if (jsonText.Contains(checkRequiredPackage))
            {
                EditorUtilities.AddScriptingDefineSymbols(EditorConstants.UNITY_ANALYTICS_ADDED, BuildTargetGroup.Android);
                EditorUtilities.AddScriptingDefineSymbols(EditorConstants.UNITY_ANALYTICS_ADDED, BuildTargetGroup.iOS);
            }
            return false;
        }

#endif
    }
}