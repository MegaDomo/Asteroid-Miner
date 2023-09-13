using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidChunkManager : MonoBehaviour
{
    [SerializeField] Grid<Chunk> chunks;

    private void Start()
    {
        if (chunks == null) Debug.Log("Chunks Null");
        if (chunks != null) Debug.Log("We're Good!!!");
    }
    public void Setup(Grid<Chunk> chunks)
    {
        this.chunks = chunks;
    }

    public Grid<Chunk> GetGrid()
    {
        return chunks;
    }
}
