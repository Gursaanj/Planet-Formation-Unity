using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject {

    public float planetRaidus = 1;
    //public NoiseSettings noiseSettings;
    public NoiseLayer[] noiseLayers;
    // additional noise settings to get layered effect 

    [System.Serializable]
    public class NoiseLayer
    {
        // toggle visibility
        public bool enabled = true;
        public NoiseSettings noiseSettings;

        // use mask of first layer to determine the possible placements for any other layer
        public bool useFirstLayerAsMask;
    }
}
