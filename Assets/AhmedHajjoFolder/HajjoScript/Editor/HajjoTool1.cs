using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//[CustomEditor(typeof(GameObject))]
public class HajjoTool1 : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button ("Generate Color"))
        {
            Debug.Log("testing");   
        }
    }
}
