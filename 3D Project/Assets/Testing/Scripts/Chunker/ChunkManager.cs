using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Script is made for Spheres currently 
public class ChunkManager : MonoBehaviour
{
    [Header("Unity References")]
    public Transform meshOrigin;
    public Material material;

    [Header("Mesh Settings")]
    [Range(1f, 100f)]
    public float radius = 3f;

    [Header("Whole Chunk Settings")]
    [Range(5f, 15f)]
    public int chunkGridSize = 5;
    public float chunkCellSize = 1f;

    [Header("Individual Chunk Settings")]
    [Range(5, 15)]
    public int marchingGridSize = 15;
    public float marchingCellSize = 1f;
    public float marchingIsoLevel = 0f;
    public bool useNoise = false;
    [Range(1f, 100f)]
    public float noiseScale = 1f;
    [Range(1f, 15f)]
    public float noiseTransform = 1f;
    public bool addCollider;
    public bool addRigidBody;

    List<ChunkMarchingCubes> chunks = new List<ChunkMarchingCubes>();

    private void Start()
    {
        CreateChunkGrid();
    }

    void CreateChunkGrid()
    {
        


        for (int x = 0; x < chunkGridSize; x++) {
            for (int y = 0; y < chunkGridSize; y++) {
                for (int z = 0; z < chunkGridSize; z++) {
                    Vector3 worldPos = new Vector3(x, y, z) * (marchingGridSize - 1);

                    GameObject clone = new GameObject("Chunk: " + x.ToString() + ", " + y.ToString() + ", " + z.ToString());
                    clone.transform.position = worldPos;

                    clone.AddComponent<MeshFilter>();
                    clone.AddComponent<MeshRenderer>().material = material;
                    ChunkMarchingCubes chunk = clone.AddComponent<ChunkMarchingCubes>();
                    clone.AddComponent<ChunkReference>().AddReference(chunk);

                    chunk.Setup(meshOrigin.position, marchingGridSize, marchingCellSize, marchingIsoLevel, 
                                radius, useNoise, noiseScale, noiseTransform);
                    chunks.Add(chunk);
                    chunk.FirstMarch(addCollider, addRigidBody);
                }
            }
        }
    }
}
