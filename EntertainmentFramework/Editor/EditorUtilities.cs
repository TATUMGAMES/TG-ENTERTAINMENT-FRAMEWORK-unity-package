#if UNITY_EDITOR
using System;
using UnityEditor;

namespace EntertainmentEditor
{
    public class EditorUtilities
    {
        /// <summary>
        /// Attempts to remove a #define constant from the Player Settings
        /// </summary>
        /// <param name="defineCompileConstant"></param>
        /// <param name="targetGroups"></param>
        public static void RemoveScriptingDefineSymbols(string defineCompileConstant, BuildTargetGroup targetGroups)
        {
            string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroups);
            int index = defines.IndexOf(defineCompileConstant);
            if (index > 0)
            {
                index -= 1;         //include the semicolon before the define
                                    //else we will remove the semicolon after the define

                //Remove the word and it's semicolon, or just the word (if listed last in defines)
                int lengthToRemove = Math.Min(defineCompileConstant.Length + 1, defines.Length - index);

                //remove the constant and it's associated semicolon (if necessary)
                defines = defines.Remove(index, lengthToRemove);

                PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroups, defines);
            }
        }

        /// <summary>
        /// Attempts to add a new #define constant to the Player Settings
        /// </summary>
        /// <param name="scriptingSymbol">constant to attempt to define</param>
        /// <param name="namedBuildTarget">platform to add this for (null will add to all platforms)</param>
        public static void AddScriptingDefineSymbols(string scriptingSymbol, BuildTargetGroup namedBuildTarget)
        {
            if (!PlayerSettings.GetScriptingDefineSymbolsForGroup(namedBuildTarget).Contains(scriptingSymbol))
            {
                if (PlayerSettings.GetScriptingDefineSymbolsForGroup(namedBuildTarget).Length != 0)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(namedBuildTarget, PlayerSettings.GetScriptingDefineSymbolsForGroup(namedBuildTarget) + ";" + scriptingSymbol);
                }
                else
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(namedBuildTarget, scriptingSymbol);
                }
            }
        }
    }
}
#endif