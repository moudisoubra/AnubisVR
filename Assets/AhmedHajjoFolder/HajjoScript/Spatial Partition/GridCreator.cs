using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hajjo;

public class GridCreator : MonoBehaviour
{
    public static GridCreator instance;
    public SpacePartitioning spacePar;

    void Start()
    {
        instance = this;
        spacePar = new SpacePartitioning(100, 100, 10, 10);
        spacePar.CreatGrid();
        Debug.Log(spacePar.GetCell(transform.position));
    }


    public void OnDrawGizmos()
    {
        for (int x = 0; x < spacePar.gridSizeX; x++)
        {
            for (int y = 0; y < spacePar.GridSizeY; y++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(new Vector3(x * spacePar.OffSetX, 0, y * spacePar.OffSetY), 4f);
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(new Vector3((x * spacePar.OffSetX) + spacePar.OffSetX * 0.5f, 0f, (y * spacePar.OffSetY) + spacePar.OffSetY * 0.5f), new Vector3(spacePar.OffSetX, 0, spacePar.OffSetX));
            }
        }
    }

}
