using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript3Grid : MonoBehaviour
{
    [SerializeField] Grid<Chunk> chunks;

    private void Start()
    {
        List<int> test = new List<int>();
        test.Add(4);
        test.Add(2);
        test.Add(7);
        test.Add(11);
        //Debug.Log(test.FindIndex(test.Contains(7));

        if (chunks == null) Debug.Log("Chunks Null");
        if (chunks != null) Debug.Log("We're Good!!!");
        Debug.Log(chunks.GetGridObject(0, 0, 0));
    }

    public void SetGrid(Grid<Chunk> test)
    {
        chunks = test;
    }
}
