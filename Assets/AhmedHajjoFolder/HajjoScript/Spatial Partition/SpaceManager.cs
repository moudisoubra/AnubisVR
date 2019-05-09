using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hajjo;
public class SpaceManager : MonoBehaviour
{

    public Vector2Int currentNode;
    public Vector2Int PreviouseNode;
    //Register To Grid Script
    private void Start()
    {
        currentNode = PreviouseNode = GridCreator.instance.spacePar.GetCell(transform.position);
        GridCreator.instance.spacePar.grid[currentNode.x, currentNode.y].ContainObjects.Add(transform);

    }

    void Update()
    {
          currentNode = GridCreator.instance.spacePar.GetCell(transform.position);

        if (currentNode != PreviouseNode)
        {
            GridCreator.instance.spacePar.grid[PreviouseNode.x, PreviouseNode.y].ContainObjects.Remove(transform);
            GridCreator.instance.spacePar.grid[currentNode.x, currentNode.y].ContainObjects.Add(transform);
        }

        PreviouseNode = currentNode;
    }



}


