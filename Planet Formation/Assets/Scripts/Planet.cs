using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


    // add range
    [Range(2,256)]
    public int resolution = 10;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    // To work in on editor
    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
        // Dont want to make 6 sided mesh filter everyime, so only called under special conditions

        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        // Create a cardinal vector to be set as localUp
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                // Keep the mesh object transform transfixed to the planet's transform 
                meshObj.transform.parent = transform;
                // Add a renderer with a default materials shader
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

            }
            //set terrain face for each side of the cube
            terrainFaces[i] = new TerrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConsructMesh();
        }
    }
}
