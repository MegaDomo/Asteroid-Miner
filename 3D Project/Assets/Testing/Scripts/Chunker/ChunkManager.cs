using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Script is made for Spheres currently 
public class ChunkManager : MonoBehaviour
{
    [Header("Unity References")]
    public Transform origin;

    [Header("Mesh Settings")]
    public float radius = 3f;

    [Header("Whole Chunk Settings")]
    public int chunkGridSize = 5;
    public float chunkCellSize = 1f;

    [Header("Individual Chunk Settings")]
    public int marchingGridSize = 15;
    public float marchingCellSize = 1f;
    public float marchingIsoLevel = 0f;
    public bool useNoise = false;
    public float noiseScale = 1f;
    public bool addCollider;
    public bool addRigidBody;

    List<ChunkMarchingCubes> chunks = new List<ChunkMarchingCubes>();

    private void Start()
    {
        CreateChunkGrid();
        LoadChunks();
    }

    void CreateChunkGrid()
    {
        for (int x = 0; x < chunkGridSize; x++) {
            for (int y = 0; y < chunkGridSize; y++) {
                for (int z = 0; z < chunkGridSize; z++) {
                    Vector3 worldPos = new Vector3(x, y, z) * chunkCellSize + 
                                       new Vector3(0f, 0f, 0f) * (marchingGridSize * marchingCellSize);

                    GameObject clone = new GameObject("Chunk: " + x.ToString() + ", " + y.ToString() + ", " + z.ToString());
                    clone.transform.position = worldPos;

                    clone.AddComponent<MeshFilter>();
                    clone.AddComponent<MeshRenderer>();
                    ChunkMarchingCubes chunk = clone.AddComponent<ChunkMarchingCubes>();

                    chunk.Setup(marchingGridSize, marchingCellSize, marchingIsoLevel);
                    chunks.Add(chunk);
                }
            }
        }
    }

    void LoadChunks()
    {
        foreach (ChunkMarchingCubes chunk in chunks)
            chunk.FirstMarch();
    }
}
