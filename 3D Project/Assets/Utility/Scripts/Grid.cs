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
                        Debug.DrawLine(GetWorldPosition(x, y, z) * cellSize, GetWorldPosition(x + 1, y, z) * cellSize, Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y, z) * cellSize, GetWorldPosition(x, y + 1, z) * cellSize, Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y, z) * cellSize, GetWorldPosition(x, y, z + 1) * cellSize, Color.white, 100f);
                    }
                }
            }

            for (int i = 0; i < width; i++)
            {
                // Verticals
                Debug.DrawLine(GetWorldPosition(i, 0, length) * cellSize, GetWorldPosition(i, height, length) * cellSize, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0, i) * cellSize, GetWorldPosition(width, height, i) * cellSize, Color.white, 100f);

                // Horizontals
                Debug.DrawLine(GetWorldPosition(0, i, length) * cellSize, GetWorldPosition(width, i, length) * cellSize, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, i, 0) * cellSize, GetWorldPosition(width, i, length) * cellSize, Color.white, 100f);
            }

            for (int i = 0; i < width + 1; i++)
            {
                // Top Layer
                Debug.DrawLine(GetWorldPosition(i, height, 0) * cellSize, GetWorldPosition(i, height, length) * cellSize, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(0, height, i) * cellSize, GetWorldPosition(width, height, i) * cellSize, Color.white, 100f);
            }
        }        
    }

    #region Utility
    public bool isCoordinatesSafe(int x, int y, int z)
    {
        if (x < 0 || y < 0 || z < 0 || x >= width || y >= height || z >= length)
            return false;

        return true;
    }

    public List<T> GetNeighbors(Vector3 position)
    {
        return GetNeighbors((int)position.x, (int)position.y, (int)position.z);
    }

    public List<T> GetNeighbors(int x, int y, int z)
    {
        List<T> items = new List<T>();

        // 26 options
        // 9 Above
        if (isCoordinatesSafe(x + 0, y + 1, z + 0)) items.Add(GetGridObject(x + 0, y + 1, z + 0));
        if (isCoordinatesSafe(x + 1, y + 1, z + 0)) items.Add(GetGridObject(x + 1, y + 1, z + 0));
        if (isCoordinatesSafe(x + 1, y + 1, z + 1)) items.Add(GetGridObject(x + 1, y + 1, z + 1));
        if (isCoordinatesSafe(x + 0, y + 1, z + 1)) items.Add(GetGridObject(x + 0, y + 1, z + 1));
        if (isCoordinatesSafe(x - 1, y + 1, z + 1)) items.Add(GetGridObject(x - 1, y + 1, z + 1));
        if (isCoordinatesSafe(x - 1, y + 1, z + 0)) items.Add(GetGridObject(x - 1, y + 1, z + 0));
        if (isCoordinatesSafe(x - 1, y + 1, z - 1)) items.Add(GetGridObject(x - 1, y + 1, z - 1));
        if (isCoordinatesSafe(x + 0, y + 1, z - 1)) items.Add(GetGridObject(x + 0, y + 1, z - 1));
        if (isCoordinatesSafe(x + 1, y + 1, z - 1)) items.Add(GetGridObject(x + 1, y + 1, z - 1));

        // 8 Middle
        if (isCoordinatesSafe(x + 1, y + 0, z + 0)) items.Add(GetGridObject(x + 1, y + 0, z + 0));
        if (isCoordinatesSafe(x + 1, y + 0, z + 1)) items.Add(GetGridObject(x + 1, y + 0, z + 1));
        if (isCoordinatesSafe(x + 0, y + 0, z + 1)) items.Add(GetGridObject(x + 0, y + 0, z + 1));
        if (isCoordinatesSafe(x - 1, y + 0, z + 1)) items.Add(GetGridObject(x - 1, y + 0, z + 1));
        if (isCoordinatesSafe(x - 1, y + 0, z + 0)) items.Add(GetGridObject(x - 1, y + 0, z + 0));
        if (isCoordinatesSafe(x - 1, y + 0, z - 1)) items.Add(GetGridObject(x - 1, y + 0, z - 1));
        if (isCoordinatesSafe(x + 0, y + 0, z - 1)) items.Add(GetGridObject(x + 0, y + 0, z - 1));
        if (isCoordinatesSafe(x + 1, y + 0, z - 1)) items.Add(GetGridObject(x + 1, y + 0, z - 1));

        // 9 Below
        if (isCoordinatesSafe(x + 0, y - 1, z + 0)) items.Add(GetGridObject(x + 0, y - 1, z + 0));
        if (isCoordinatesSafe(x + 1, y - 1, z + 0)) items.Add(GetGridObject(x + 1, y - 1, z + 0));
        if (isCoordinatesSafe(x + 1, y - 1, z + 1)) items.Add(GetGridObject(x + 1, y - 1, z + 1));
        if (isCoordinatesSafe(x + 0, y - 1, z + 1)) items.Add(GetGridObject(x + 0, y - 1, z + 1));
        if (isCoordinatesSafe(x - 1, y - 1, z + 1)) items.Add(GetGridObject(x - 1, y - 1, z + 1));
        if (isCoordinatesSafe(x - 1, y - 1, z + 0)) items.Add(GetGridObject(x - 1, y - 1, z + 0));
        if (isCoordinatesSafe(x - 1, y - 1, z - 1)) items.Add(GetGridObject(x - 1, y - 1, z - 1));
        if (isCoordinatesSafe(x + 0, y - 1, z - 1)) items.Add(GetGridObject(x + 0, y - 1, z - 1));
        if (isCoordinatesSafe(x + 1, y - 1, z - 1)) items.Add(GetGridObject(x + 1, y - 1, z - 1));

        return items;
    }
    #endregion


    #region Getters & Setters
    public T GetGridObject(int x, int y, int z)
    {
        if (!isCoordinatesSafe(x, y, z))
            return default(T);

        return gridArray[x, y, z];
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize;
    }

    public void GetXYZ(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
        z = Mathf.FloorToInt(worldPosition.z / cellSize);
    }

    public void SetGridObject(Vector3 worldPosition, T newGridObject)
    {
        int x, y, z;
        GetXYZ(worldPosition, out x, out y, out z);
        SetGridObject(x, y, z, newGridObject);
    }

    public void SetGridObject(int x, int y, int z, T newGridObject)
    {
        if (!isCoordinatesSafe(x, y, z))
            return;
        gridArray[x, y, z] = newGridObject;
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
    #endregion
}
