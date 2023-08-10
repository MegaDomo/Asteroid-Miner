using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkReference2 : MonoBehaviour
{
    public Chunk chunk;

    public void AddReference(Chunk chunk)
    {
        this.chunk = chunk;
    }
}
