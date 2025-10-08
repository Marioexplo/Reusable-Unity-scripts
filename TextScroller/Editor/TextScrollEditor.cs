using UnityEditor;
using UnityEngine;

namespace TextScroller
{
    [CustomEditor(typeof(TextScroll))]
    public class TextScrollEditor : Editor
    {
        private Options option = Options.Simple;
        private SerializedProperty current;
        private string Current
        {
            get => current.stringValue;
            set => current.stringValue = value;
        }
        private SerializedProperty array;
        private SerializedProperty branches;
        private SerializedProperty script;

        void OnEnable()
        {
            if (EditorApplication.isPlaying)
            {
                Destroy(this);
                return;
            }
            current = serializedObject.FindProperty("current");
            array = serializedObject.FindProperty("array");
            branches = serializedObject.FindProperty("branches");
            script = serializedObject.FindProperty("m_Script");
            int arrayLenght = array.arraySize;
            for (int i = 0; i < arrayLenght; i++) if (array.GetArrayElementAtIndex(0).stringValue == Current)
                {
                    option = Options.Array;
                    return;
                }
            arrayLenght = branches.arraySize;
            for (int i = 0; i < arrayLenght; i++)
            {
                if (branches.GetArrayElementAtIndex(0).FindPropertyRelative("name").stringValue == Current)
                    option = Options.Branches;
            }
        }

        public override void OnInspectorGUI()
        {
            if (EditorApplication.isPlaying)
            {
                DrawDefaultInspector();
                return;
            }
            serializedObject.Update();
            GUI.enabled = false;
            EditorGUILayout.PropertyField(script);
            GUI.enabled = true;
            option = (Options)EditorGUILayout.EnumPopup(new GUIContent("Type", "The kind of scrolling"), option, EditorStyles.popup);
            switch (option)
            {
                case Options.Simple:
                    DrawPropertiesExcluding(serializedObject, "m_Script", "array", "autoArray", "pauseTime", "branches");
                    break;
                case Options.Array:
                    DrawPropertiesExcluding(serializedObject, "m_Script", "current", "branches");
                    Current = array.arraySize == 0 ? string.Empty : array.GetArrayElementAtIndex(0).stringValue;
                    break;
                case Options.Branches:
                    DrawPropertiesExcluding(serializedObject, "m_Script", "current", "array", "autoArray", "pauseTime");
                    Current = branches.arraySize == 0 ? string.Empty :
                        branches.GetArrayElementAtIndex(0).FindPropertyRelative("text").stringValue;
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }

        [System.Serializable]
        private enum Options { Simple, Array, Branches }
    }
}
