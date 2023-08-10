using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript3Grid : MonoBehaviour
{
    Grid<Node> grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<Node>(10, 10, 10, 3, Vector3.zero, () => new Node());
    }
}
