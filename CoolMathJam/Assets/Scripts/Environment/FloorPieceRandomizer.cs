using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FloorPieceRandomizer : MonoBehaviour
{
    public Mesh[] TopMeshes;
    public Mesh[] FrontMeshes;
    public MeshFilter TopMeshFilter;
    public MeshFilter FrontMeshFilter;

    void Start()
    {
        int TopMeshIndex = Random.Range(0, TopMeshes.Length);
        int FrontMeshIndex = Random.Range(0, FrontMeshes.Length);
        TopMeshFilter.mesh = TopMeshes[TopMeshIndex];
        FrontMeshFilter.mesh = FrontMeshes[FrontMeshIndex];
    }
}
