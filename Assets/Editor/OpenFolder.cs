using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Puzzle), true)]
public class NewBehaviourScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Select Folder"))
        {
            Puzzle p = (Puzzle) target;
            p.hintFolderPath = EditorUtility.OpenFolderPanel("Select a sprite folder", "", "").Substring(Application.dataPath.Length + 11);
        }
    }
}
