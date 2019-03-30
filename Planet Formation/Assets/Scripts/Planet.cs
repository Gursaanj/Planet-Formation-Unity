using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {


    // add range
    [Range(2,256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    // foldout bool

    [HideInInspector]
    public bool shapeSettingFoldout;

    [HideInInspector]
    public bool colourSettingFoldout;

    //Take into account details of the Sphere for generation
    ShapeGenerator shapeGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    // To work in on editor

    //private void OnValidate()
    //{
    //    GeneratePlanet();
    //}

    void Initialize()
    {
        // Dont want to make 6 sided mesh filter everyime, so only called under special conditions

        shapeGenerator = new ShapeGenerator(shapeSettings);
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
            terrainFaces[i] = new TerrainFace(shapeGenerator,meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    public void GeneratePlanet()  // called to Generate planet
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }
    
    public void onShapeSettingsUpdated() // called only when the shape setting changed
    {
        if (autoUpdate)
        {

            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated() // called only when the colour setting are changed
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConsructMesh();
        }
    }

    void GenerateColours()
    {
        // loop through meshes and sets the colour to the desired colour from colourSettings
        foreach (MeshFilter m in meshFilters)
        {
            m.GetComponent<MeshRenderer>().sharedMaterial.color = colourSettings.planetColour;
        }
    }
}
