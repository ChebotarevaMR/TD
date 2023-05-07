using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(Location.Location))]
    public class EditorLocation : UnityEditor.Editor {
        public override void OnInspectorGUI(){
            if (GUILayout.Button("Generate field")){
                ((Location.Location) target).GenerateField();
            }
        }
    }
}