using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomHint), true)]
public class NewBehaviourScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Select Folder"))
        {
            RandomHint rh = (RandomHint) target;
            rh.folderPath = EditorUtility.OpenFolderPanel("Select a sprite folder", "", "").Substring(Application.dataPath.Length + 11);
        }
    }
}
