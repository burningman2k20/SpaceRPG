using System.Collections;
using System.Collections.Generic;

//C# Example (LookAtPointEditor.cs)
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DCDialog))]
//[CanEditMultipleObjects]
public class DCDialogEditor : Editor 
{
    SerializedProperty lookAtPoint;
    SerializedProperty canGoBack;

    
    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
        canGoBack = serializedObject.FindProperty("canGoBack");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(lookAtPoint);
        EditorGUILayout.PropertyField(canGoBack);

        
        serializedObject.ApplyModifiedProperties();
    }
}
