using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{

    public Vector2Int currentNode;
    public Vector2Int PreviouseNode;

    //Register To Grid Script
    private void Start()
    {
        currentNode = PreviouseNode = Grid.instance.GetCell(transform.position);
        Grid.instance.grid[currentNode.x, currentNode.y].ContainObjects.Add(transform);

    }

    void Update()
    {
          currentNode = Grid.instance.GetCell(transform.position);

        if (currentNode != PreviouseNode)
        {
            Grid.instance.grid[PreviouseNode.x, PreviouseNode.y].ContainObjects.Remove(transform);
            Grid.instance.grid[currentNode.x, currentNode.y].ContainObjects.Add(transform);
        }

        PreviouseNode = currentNode;
    }



}


