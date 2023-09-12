#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Serialize.utils;

// This Script Creates Asteroids
[ExecuteInEditMode]
public class ChunkManager2 : MonoBehaviour
{
    [Header("Unity References")]
    public Transform meshOrigin;
    public Material material;

    [Header("Mesh Settings")]
    public bool serialize = false;
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

    public void CreateChunkGrid()
    {
        Grid<Chunk> chunks = new Grid<Chunk>(chunkGridSize, chunkGridSize, chunkGridSize, chunkCellSize, meshOrigin.position, () => new Chunk());
        
        GameObject obj = new GameObject("Asteroid");
        Transform parent = obj.transform;
        parent.position = transform.position;
        
        for (int x = 0; x < chunkGridSize; x++) {
            for (int y = 0; y < chunkGridSize; y++) {
                for (int z = 0; z < chunkGridSize; z++) {
                    Vector3 worldPos = new Vector3(x, y, z) * (marchingGridSize - 1);

                    GameObject clone = new GameObject("Chunk: " + x.ToString() + ", " + y.ToString() + ", " + z.ToString());
                    clone.transform.position = worldPos;
                    clone.transform.SetParent(parent);
                    clone.AddComponent<MeshFilter>();
                    clone.AddComponent<MeshRenderer>().material = material;
                    clone.AddComponent<SerializeMesh>();
                    Chunk chunk = clone.AddComponent<Chunk>();
                    clone.AddComponent<ChunkReference2>().AddReference(chunk);

                    ChunkData chunkData = CreateChunkData(worldPos, x, y, z);
                    chunk.Setup(chunkData, chunks);

                    chunks.SetGridObject(x, y, z, chunk);

                    chunk.FirstMarch(addCollider, addRigidBody);

                    if (serialize) chunk.GetComponent<SerializeMesh>().Serialize();
                }
            }
        }
    }

    ChunkData CreateChunkData(Vector3 worldPos, int x, int y, int z)
    {
        ChunkData data = new ChunkData();

        data.gridOrigin = meshOrigin.position;
        data.chunkOrigin = worldPos;
        data.gridCoord = new Vector3(x, y, z);

        data.gridSize = marchingGridSize;
        data.cellSize = marchingCellSize;
        data.isoLevel = marchingIsoLevel;

        data.radius = radius;

        data.useNoise = useNoise;
        data.noiseScale = noiseScale;
        data.noiseTransform = noiseTransform;

        return data;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ChunkManager2))]
public class ChunkManager2Editor : Editor
{
    ChunkManager2 obj;

    void OnSceneGUI()
    {
        obj = (ChunkManager2)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Asteroid"))
        {
            obj.CreateChunkGrid();
        }
    }
}
#endif