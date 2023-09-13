using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Node<T>
{
    [SerializeField] public Vector3 coord;
    [SerializeField] public T element;
    public Node(Vector3 coord, T element)
    {
        this.coord = coord;
        this.element = element;
    }
}

// 3D WorldSpace Data Structure
[System.Serializable]
public class Grid<T>
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int length;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 origin;
    [SerializeField] private List<Vector3> coordinates;
    [SerializeField] private List<T> elements;
    [SerializeField] private T[,,] gridArray;

    // Constructor
    public Grid(int width, int height, int length, float cellSize, Vector3 origin, Func<T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.length = length;
        this.cellSize = cellSize;
        this.origin = origin;

        gridArray = new T[width, height, length];
        coordinates = new List<Vector3>();
        elements = new List<T>();

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < length; z++) {
                    coordinates.Add(new Vector3(x, y, z));
                    elements.Add(createGridObject());
                }
            }
        }
/*
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                for (int z = 0; z < length; z++)
                    gridArray[x, y, z] = createGridObject();
*/
        bool debug = false;
        if (debug)
        {
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    for (int z = 0; z < length; z++) {
                        //Utils.CreateWorldText(GetWorldPosition(x, y, z) + new Vector3(cellSize, 0, cellSize) * 0.5f, x.ToString() + ", " + y.ToString() + ", " + z.ToString(), 30, TextAnchor.MiddleCenter);
                        Debug.DrawLine(GetWorldPosition(x, y, z) * 10 *cellSize, GetWorldPosition(x + 1, y, z) * 10 * cellSize, Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y, z) * 10 * cellSize, GetWorldPosition(x, y + 1, z) * 10 * cellSize, Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(x, y, z) * 10 * cellSize, GetWorldPosition(x, y, z + 1) * 10 * cellSize, Color.white, 100f);
                    }
                }
            }

            for (int i = 0; i < width; i++) {
                // Verticals
                Debug.DrawLine(GetWorldPosition(i, 0, length) * 10 * cellSize, GetWorldPosition(i, height, length) * 10 * cellSize, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, 0, i) * 10 * cellSize, GetWorldPosition(width, height, i) * 10 * cellSize, Color.white, 100f);

                // Horizontals
                Debug.DrawLine(GetWorldPosition(0, i, length) * 10 * cellSize, GetWorldPosition(width, i, length) * 10 * cellSize, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(width, i, 0) * 10 * cellSize, GetWorldPosition(width, i, length) * 10 * cellSize, Color.white, 100f);

                // Top Layer
                Debug.DrawLine(GetWorldPosition(i, height, 0) * 10 * cellSize, GetWorldPosition(i, height, length) * 10 * cellSize, Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(0, height, i) * 10 * cellSize, GetWorldPosition(width, height, i) * 10 * cellSize, Color.white, 100f);
            }
        }        
    }

    public Grid(int width, int height, int length, Func<T> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.length = length;

        gridArray = new T[width, height, length];
        coordinates = new List<Vector3>();
        elements = new List<T>();
        
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < length; z++) {
                    coordinates.Add(new Vector3(x, y, z));
                    elements.Add(createGridObject());
                }
            }
        }
            
                
            
                
/*

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                for (int z = 0; z < length; z++)
                    gridArray[x, y, z] = createGridObject();*/
    }

    public Grid()
    {

    }

    #region Howdy Neighbor
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

    public List<T> GetClosestNeighbors(Vector3 playerPoint, float chunkGridSize, float chunkCellSize, int x, int y, int z)
    {
        List<T> items = GetNeighbors(x, y, z);
        Vector3 gridCenter = (GetWorldPosition(x, y, z) * chunkGridSize) + (new Vector3(chunkGridSize, chunkGridSize, chunkGridSize) / 2) * chunkCellSize;
        Debug.Log("Origin Chunk: " + new Vector3(x, y, z) +
                  "World Position: " + GetWorldPosition(x, y, z) * chunkGridSize);
        // TODO : Double Check whether gridCenter or PlayerPoint are actually comparable
        // TODO : Also check if the wrong 7 neighbors are getting set up, perhaps with <= || >=
        #region X
        if (playerPoint.x < gridCenter.x)
        {
            // Remove item that are Greater than x
            if (isCoordinatesSafe(x + 1, y + 1, z + 1)) items.Remove(GetGridObject(x + 1, y + 1, z + 1));
            if (isCoordinatesSafe(x + 1, y + 1, z + 0)) items.Remove(GetGridObject(x + 1, y + 1, z + 0));
            if (isCoordinatesSafe(x + 1, y + 1, z - 1)) items.Remove(GetGridObject(x + 1, y + 1, z - 1));
            if (isCoordinatesSafe(x + 1, y + 0, z + 1)) items.Remove(GetGridObject(x + 1, y + 0, z + 1));
            if (isCoordinatesSafe(x + 1, y + 0, z + 0)) items.Remove(GetGridObject(x + 1, y + 0, z + 0));
            if (isCoordinatesSafe(x + 1, y + 0, z - 1)) items.Remove(GetGridObject(x + 1, y + 0, z - 1));
            if (isCoordinatesSafe(x + 1, y - 1, z + 1)) items.Remove(GetGridObject(x + 1, y - 1, z + 1));
            if (isCoordinatesSafe(x + 1, y - 1, z + 0)) items.Remove(GetGridObject(x + 1, y - 1, z + 0));
            if (isCoordinatesSafe(x + 1, y - 1, z - 1)) items.Remove(GetGridObject(x + 1, y - 1, z - 1));
        }
        else if (playerPoint.x > gridCenter.x)
        {
            // Remove item that are Lesser than x
            if (isCoordinatesSafe(x - 1, y + 1, z + 1)) items.Remove(GetGridObject(x + 1, y + 1, z + 1));
            if (isCoordinatesSafe(x - 1, y + 1, z + 0)) items.Remove(GetGridObject(x + 1, y + 1, z + 0));
            if (isCoordinatesSafe(x - 1, y + 1, z - 1)) items.Remove(GetGridObject(x + 1, y + 1, z - 1));
            if (isCoordinatesSafe(x - 1, y + 0, z + 1)) items.Remove(GetGridObject(x + 1, y + 0, z + 1));
            if (isCoordinatesSafe(x - 1, y + 0, z + 0)) items.Remove(GetGridObject(x + 1, y + 0, z + 0));
            if (isCoordinatesSafe(x - 1, y + 0, z - 1)) items.Remove(GetGridObject(x + 1, y + 0, z - 1));
            if (isCoordinatesSafe(x - 1, y - 1, z + 1)) items.Remove(GetGridObject(x + 1, y - 1, z + 1));
            if (isCoordinatesSafe(x - 1, y - 1, z + 0)) items.Remove(GetGridObject(x + 1, y - 1, z + 0));
            if (isCoordinatesSafe(x - 1, y - 1, z - 1)) items.Remove(GetGridObject(x + 1, y - 1, z - 1));
        }
        #endregion

        #region Y
        if (playerPoint.y < gridCenter.y)
        {
            // Remove item that are Greater than y
            if (isCoordinatesSafe(x + 0, y + 1, z + 0)) items.Remove(GetGridObject(x + 0, y + 1, z + 0));
            if (isCoordinatesSafe(x + 1, y + 1, z + 0)) items.Remove(GetGridObject(x + 1, y + 1, z + 0));
            if (isCoordinatesSafe(x + 1, y + 1, z + 1)) items.Remove(GetGridObject(x + 1, y + 1, z + 1));
            if (isCoordinatesSafe(x + 0, y + 1, z + 1)) items.Remove(GetGridObject(x + 0, y + 1, z + 1));
            if (isCoordinatesSafe(x - 1, y + 1, z + 1)) items.Remove(GetGridObject(x - 1, y + 1, z + 1));
            if (isCoordinatesSafe(x - 1, y + 1, z + 0)) items.Remove(GetGridObject(x - 1, y + 1, z + 0));
            if (isCoordinatesSafe(x - 1, y + 1, z - 1)) items.Remove(GetGridObject(x - 1, y + 1, z - 1));
            if (isCoordinatesSafe(x + 0, y + 1, z - 1)) items.Remove(GetGridObject(x + 0, y + 1, z - 1));
            if (isCoordinatesSafe(x + 1, y + 1, z - 1)) items.Remove(GetGridObject(x + 1, y + 1, z - 1));
        }
        else if (playerPoint.y > gridCenter.y)
        {
            // Remove item that are Lesser than y
            if (isCoordinatesSafe(x + 0, y - 1, z + 0)) items.Remove(GetGridObject(x + 0, y - 1, z + 0));
            if (isCoordinatesSafe(x + 1, y - 1, z + 0)) items.Remove(GetGridObject(x + 1, y - 1, z + 0));
            if (isCoordinatesSafe(x + 1, y - 1, z + 1)) items.Remove(GetGridObject(x + 1, y - 1, z + 1));
            if (isCoordinatesSafe(x + 0, y - 1, z + 1)) items.Remove(GetGridObject(x + 0, y - 1, z + 1));
            if (isCoordinatesSafe(x - 1, y - 1, z + 1)) items.Remove(GetGridObject(x - 1, y - 1, z + 1));
            if (isCoordinatesSafe(x - 1, y - 1, z + 0)) items.Remove(GetGridObject(x - 1, y - 1, z + 0));
            if (isCoordinatesSafe(x - 1, y - 1, z - 1)) items.Remove(GetGridObject(x - 1, y - 1, z - 1));
            if (isCoordinatesSafe(x + 0, y - 1, z - 1)) items.Remove(GetGridObject(x + 0, y - 1, z - 1));
            if (isCoordinatesSafe(x + 1, y - 1, z - 1)) items.Remove(GetGridObject(x + 1, y - 1, z - 1));
        }
        #endregion

        #region Z
        if (playerPoint.z < gridCenter.z)
        {
            // Remove item that are Greater than z
            if (isCoordinatesSafe(x + 1, y - 1, z + 1)) items.Remove(GetGridObject(x + 1, y - 1, z + 1));
            if (isCoordinatesSafe(x + 0, y - 1, z + 1)) items.Remove(GetGridObject(x + 0, y - 1, z + 1));
            if (isCoordinatesSafe(x - 1, y - 1, z + 1)) items.Remove(GetGridObject(x - 1, y - 1, z + 1));
            if (isCoordinatesSafe(x + 1, y + 0, z + 1)) items.Remove(GetGridObject(x + 1, y + 0, z + 1));
            if (isCoordinatesSafe(x + 0, y + 0, z + 1)) items.Remove(GetGridObject(x + 0, y + 0, z + 1));
            if (isCoordinatesSafe(x - 1, y + 0, z + 1)) items.Remove(GetGridObject(x - 1, y + 0, z + 1));
            if (isCoordinatesSafe(x + 1, y + 1, z + 1)) items.Remove(GetGridObject(x + 1, y + 1, z + 1));
            if (isCoordinatesSafe(x + 0, y + 1, z + 1)) items.Remove(GetGridObject(x + 0, y + 1, z + 1));
            if (isCoordinatesSafe(x - 1, y + 1, z + 1)) items.Remove(GetGridObject(x - 1, y + 1, z + 1));
        }
        else if (playerPoint.z > gridCenter.z)
        {
            // Remove item that are Lesser than z
            if (isCoordinatesSafe(x - 1, y - 1, z - 1)) items.Remove(GetGridObject(x - 1, y - 1, z - 1));
            if (isCoordinatesSafe(x + 0, y - 1, z - 1)) items.Remove(GetGridObject(x + 0, y - 1, z - 1));
            if (isCoordinatesSafe(x + 1, y - 1, z - 1)) items.Remove(GetGridObject(x + 1, y - 1, z - 1));
            if (isCoordinatesSafe(x - 1, y + 0, z - 1)) items.Remove(GetGridObject(x - 1, y + 0, z - 1));
            if (isCoordinatesSafe(x + 0, y + 0, z - 1)) items.Remove(GetGridObject(x + 0, y + 0, z - 1));
            if (isCoordinatesSafe(x + 1, y + 0, z - 1)) items.Remove(GetGridObject(x + 1, y + 0, z - 1));
            if (isCoordinatesSafe(x - 1, y + 1, z - 1)) items.Remove(GetGridObject(x - 1, y + 1, z - 1));
            if (isCoordinatesSafe(x + 0, y + 1, z - 1)) items.Remove(GetGridObject(x + 0, y + 1, z - 1));
            if (isCoordinatesSafe(x + 1, y + 1, z - 1)) items.Remove(GetGridObject(x + 1, y + 1, z - 1));
        }
        #endregion
        return items;
    }
    #endregion

    #region Getters & Setters
    public T GetGridObject(int x, int y, int z)
    {
        if (!isCoordinatesSafe(x, y, z))
            return default(T);

        int index = coordinates.IndexOf(new Vector3(x, y, z));
        return elements[index];
        return gridArray[x, y, z];
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize; // Shouldn't this be +Origin
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

        int index = coordinates.IndexOf(new Vector3(x, y, z));
        elements[index] = newGridObject;

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
