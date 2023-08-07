using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkReference : MonoBehaviour
{
    public ChunkMarchingCubes chunkMarchingCubes;

    public void AddReference(ChunkMarchingCubes chunkMarchingCubes)
    {
        this.chunkMarchingCubes = chunkMarchingCubes;
    }
}
