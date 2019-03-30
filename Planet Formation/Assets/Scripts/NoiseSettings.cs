using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Want to show up in the inspector
[System.Serializable]
public class NoiseSettings
{
    public float strength = 1;
    // Will use layering model to keep persistance of main shape
    [Range(1,8)]
    public int numLayers = 1;
    public float baseRoughness = 1;
    public float roughness = 2;
    // the amplitude of each layer is multiplied by persistance
    public float persistance = .5f;
    public Vector3 centre;

    public float minValue;

}
