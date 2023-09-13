using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidChunkManager : MonoBehaviour
{
    [SerializeField] Grid<Chunk> chunks;

    public void Setup(Grid<Chunk> chunks)
    {
        this.chunks = chunks;
    }

    public Grid<Chunk> GetGrid()
    {
        return chunks;
    }
}
