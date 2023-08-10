using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3D WorldSpace Data Structure
public class Grid<T>
{
    private int width;
    private int height;
    private int length;
    private float cellSize;
    private Vector3 origin;
    private T[,,] gridArray;

    // Constructor
    public Grid(int width, int height, int length, float cellSize, Vector3 origin, Func<T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.length = length;
        this.cellSize = cellSize;
        this.origin = origin;

        gridArray = new T[width, height, length];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                for (int z = 0; z < length; z++)
                    gridArray[x, y, z] = createGridObject();

        bool debug = true;
        if (debug)
        {
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    for (int z = 0; z < length; z++) {
                        //Utils.CreateWorldText(GetWorldPosition(x, y, z) + new Vector3(cellSize, 0, cellSize) * 0.5f, x.ToString() + ", " + y.ToString() + ", " + z.ToString(), 30, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y, z), GetWorldPosition(x + 1, y, z), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y, z), GetWorldPosition(x, y + 1, z), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y, z), GetWorldPosition(x, y, z + 1), Color.white, 100f);
                    }
                }
            }

            for (int i = 0; i < width; i++)
            {
                // Verticals
                Debug.DrawLine(GetWorldPosition(i, 0, length), GetWorldPosition(i, height, length), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0, i), GetWorldPosition(width, height, i), Color.white, 100f);

                // Horizontals
                Debug.DrawLine(GetWorldPosition(0, i, length), GetWorldPosition(width, i, length), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, i, 0), GetWorldPosition(width, i, length), Color.white, 100f);
            }

            for (int i = 0; i < width + 1; i++)
            {
                // Top Layer
                Debug.DrawLine(GetWorldPosition(i, height, 0), GetWorldPosition(i, height, length), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(0, height, i), GetWorldPosition(width, height, i), Color.white, 100f);
            }
        }        
    }

    public T GetGridObject(int x, int y, int z)
    {
        if (!isCoordinatesSafe(x, z))
            return default(T);

        return gridArray[x, y, z];
    }

    public Vector3 GetWorldPosition(Node node)
    {
        return new Vector3(node.x, node.y, node.z) * cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public void GetXYZ(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public int GetSize()
    {
        return width;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetLength()
    {
        return length;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public void SetGridObject(Vector3 worldPosition, T newGridObject)
    {
        int x, y, z;
        GetXZ(worldPosition, out x, out y, out z);
        SetGridObject(x, y, z, newGridObject);
    }

    public void SetGridObject(int x, int y, int z, T newGridObject)
    {
        if (!isCoordinatesSafe(x, z))
            return;
        gridArray[x, y, z] = newGridObject;
    }

    public bool isCoordinatesSafe(int x, int z)
    {
        if (x < 0 || z < 0 || x >= width || z >= height)
            return false;

        return true;
    }
}
