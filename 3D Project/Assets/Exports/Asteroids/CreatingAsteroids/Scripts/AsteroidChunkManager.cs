using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidChunkManager : MonoBehaviour
{
    Grid<Chunk> chunks;
    Grid<ChunkData> data;

    public void Setup(Grid<Chunk> chunks, Grid<ChunkData> data)
    {
        this.chunks = chunks;
        this.data = data;
    }

    public Grid<Chunk> GetGrid()
    {
        return chunks;
    }

    public Grid<ChunkData> GetData()
    {
        return data;
    }
}
