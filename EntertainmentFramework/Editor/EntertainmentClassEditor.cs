#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EntertainmentEditor
{
    [CustomEditor(typeof(EntertainmentFrameworkSettings))]
    public class EntertainmentClassEditor : Editor
    {
        private Color bgColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);

        public override void OnInspectorGUI()
        {
            WrapperHeaderSetupEditor();
            WrapperSetupEditor();

            serializedObject.ApplyModifiedProperties();
        }

        private void WrapperHeaderSetupEditor()
        {
            GUIStyle headingStyle = new GUIStyle
            {
                fontSize = 28,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };
            headingStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(10, 5, 60, 60), EditorConstants.Entertainement.ToUpper(), headingStyle);
            EditorGUILayout.Space(50);

            var rectPos = EditorGUILayout.BeginHorizontal();
            Handles.DrawLine(new Vector2(rectPos.x - 30, rectPos.y), new Vector2(rectPos.width + 20, rectPos.y));
            EditorGUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
        }

        private void WrapperSetupEditor()
        {
            var rectPos = EditorGUILayout.BeginVertical();
            Handles.DrawSolidRectangleWithOutline(new Rect(rectPos.x - 10, rectPos.y + 5, rectPos.width + 10, rectPos.height), bgColor, bgColor);
            EditorGUILayout.Space(5);
            GUIStyle headingStyle = new GUIStyle
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold
            };
            headingStyle.normal.textColor = Color.white;
            GUI.Label(new Rect(20, 70, 60, 160), EditorConstants.UnityAnaltyicsSetupHeader.ToUpper(), headingStyle);
            EditorGUILayout.Space(35);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("unityAnalytics"), new GUIContent(EditorConstants.UnityAnaltyicsLabel));
            EditorGUILayout.Space(25);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("initalizeUnityAnalytics"), new GUIContent(EditorConstants.AutoInitializeUnityAnalytics));
            EditorGUILayout.Space(5);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("sendUnityAnalytics"), new GUIContent(EditorConstants.AutoTrackEventData));
            EditorGUILayout.EndVertical();
        }
    }
}
#endif