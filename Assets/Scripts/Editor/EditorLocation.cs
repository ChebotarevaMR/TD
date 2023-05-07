using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(Location.Location))]
    public class EditorLocation : UnityEditor.Editor {
        private SerializedProperty generationField;
        public override void OnInspectorGUI(){
            generationField = serializedObject.FindProperty("generationField");
            EditorGUILayout.PropertyField(generationField, new GUIContent("Gen field"));
            if (GUILayout.Button("Generate field")){
                ((Location.Location) target).GenerateField();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}