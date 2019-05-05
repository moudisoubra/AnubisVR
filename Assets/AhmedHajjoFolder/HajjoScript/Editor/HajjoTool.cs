using UnityEngine;
using UnityEditor;

public class HajjoTool : EditorWindow
{
    string mystring = "Hello Hajjo";
    Color color;


    [MenuItem("Window/Test")]
    public static void showWindow()
    {

        EditorWindow.GetWindow<HajjoTool>("Example");

    }
    void OnGUI()
    {
        GUILayout.Label("Change Color of ", EditorStyles.boldLabel);
        mystring = EditorGUILayout.TextField("Name", mystring);

        color = EditorGUILayout.ColorField("Color", color);

        if (GUILayout.Button("COLORIZE"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                 if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
        }
    }
}


