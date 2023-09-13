#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Serialize.utils
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    public class SerializeMesh : MonoBehaviour
    {
        [HideInInspector] [SerializeField] Vector2[] uv;
        [HideInInspector] [SerializeField] Vector3[] verticies;
        [HideInInspector] [SerializeField] int[] triangles;
        [HideInInspector] [SerializeField] bool serialized = false;
        // Use this for initialization

        void Awake()
        {
            if (serialized)
            {
                GetComponent<MeshFilter>().mesh = Rebuild();
            }
        }

        void Start()
        {
            if (serialized) return;

            //Serialize();
        }

        public void Serialize()
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            if (mesh == null) return;

            uv = mesh.uv;
            verticies = mesh.vertices;
            triangles = mesh.triangles;

            serialized = true;
        }

        public Mesh Rebuild()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.uv = uv;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            GetComponent<MeshCollider>().sharedMesh = mesh;
            return mesh;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SerializeMesh))]
    class SerializeMeshEditor : Editor
    {
        SerializeMesh obj;

        void OnSceneGUI()
        {
            obj = (SerializeMesh)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Rebuild")) {
                if (obj)
                    if (obj.gameObject.GetComponent<MeshFilter>().mesh != null)
                        obj.gameObject.GetComponent<MeshFilter>().mesh = obj.Rebuild();
            }

            if (GUILayout.Button("Serialize")) {
                if (obj)
                    if (obj.gameObject.GetComponent<MeshFilter>().mesh != null)
                        obj.Serialize();
            }
        }
    }
#endif
}