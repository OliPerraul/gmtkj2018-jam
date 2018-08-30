﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public Vector2Int size = new Vector2Int(256, 256);
    public int cellSize = 2;

    public BoxCollider collider;

    public GameObject cellPrototype;
    public GameObject cellsParent;
    public Cell[,] cells;


    // Use this for initialization
    void Start()
    {
        cells = new Cell[(int)size.x, (int)size.y];
        RefreshGrid();
    }

    void RefreshGrid()
    {
        Vector3 start = cellPrototype.transform.position;

        //update collider pos
        collider.size = new Vector3(size.x * cellSize, 1, size.y * cellSize);
        //collider.transform.position = start;
        Vector3 center = (collider.size / 2);
        center.x -= 1; center.z -= 1;
        center.y = 0;
       
        collider.center = center;
       

        GameObjectUtil.DestroyImmediateChildren(cellsParent);
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                GameObject o = GameObject.Instantiate(cellPrototype.gameObject, cellsParent.transform);
                o.transform.position = new Vector3(start.x + j * cellSize, start.y, start.z + i * cellSize);
                cells[j, i] = o.GetComponent<Cell>();
             }

        }

    }




}
