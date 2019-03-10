using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlacementTool : EditorWindow
{

    Vector3 MousePOS;
    public static float depth;
    public static bool placeObjects = false;
    public static bool is2D;
    public static GameObject chosenObject;
    public static GameObject tempObject;
    private static new Vector2 position;
    static Editor placementToolEditor;
    public static Vector3 placePos;
    public static Vector3 newPos;
    public static Quaternion placeRot;
    public SceneView SV;
    public string mouseOver = "Nothing...";
    public enum menuOption { menu1, menu2, nomenu };
    public static menuOption optionChosen;
    public GUIStyle bgColor;

    [MenuItem("Placement Tool/ Place a Prefab!")]
    public static void ShowWindow()
    {
        GetWindow<PlacementTool>("Placement Tool!");
    }


    public static Vector2 Position
    {
        get { return position; }
    }

    static PlacementTool()
    {
        SceneView.onSceneGUIDelegate += UpdateView;
    }

    private static void UpdateView(SceneView sceneView)
    {
        if (Event.current != null)
            position = new Vector2(Event.current.mousePosition.x + sceneView.position.x,
                Event.current.mousePosition.y + sceneView.position.y);


        if (Event.current.button == 0 && Event.current.type == EventType.MouseDown && placeObjects)
        {
            Vector3 genPos = Vector3.zero;
            //Ray ray = Camera.current.ScreenPointToRay(position);
            RaycastHit hit;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            

            if (sceneView.in2DMode)
            {
                genPos = Camera.current.ScreenToWorldPoint(new Vector3(position.x, (Screen.height + 35) - position.y, depth));
                genPos.z = 0;
            }
            else if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                if (hit.transform.gameObject.GetComponent<Collider>())
                {
                    genPos = (hit.point + (hit.normal * depth));
                }
                else
                {
                    genPos = hit.point;
                }
                Debug.Log("You selected the " + hit.transform.name);
            }
            else
                genPos = Camera.current.ViewportToWorldPoint(new Vector3(position.x / Screen.width, (1.1f - position.y / Camera.current.pixelHeight), depth));


            GameObject.Instantiate(chosenObject, genPos, Quaternion.identity);
        }
    }

    

    void Update()
    {

    }
    
    public void Display()
    {
        Texture2D background = new Texture2D(128, 128);

        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {
                background.SetPixel(x, y, new Color(0.2f, 0.2f, 0.2f));
            }
        }

        background.Apply();
        bgColor = new GUIStyle();
        bgColor.normal.background = background;
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        chosenObject = (GameObject)EditorGUILayout.ObjectField("Selected GameObject: ", chosenObject, typeof(GameObject), true);
        if (EditorGUI.EndChangeCheck())
        {
            Object.DestroyImmediate(placementToolEditor);// The begin and end check checks for change in that object field
        }


        depth = EditorGUILayout.FloatField("Depth Value: ", depth);
        placeObjects = EditorGUILayout.Toggle("Place Objects? ", placeObjects);

        if (chosenObject != null)
        {
            if (placementToolEditor == null)
                placementToolEditor = Editor.CreateEditor(chosenObject);

            Display();
            placementToolEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(500, 300), bgColor);
        }
    }
}
