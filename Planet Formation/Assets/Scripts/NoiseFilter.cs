using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilter {

    Noise noise = new Noise();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        /*
        but evaluted noise between 0 and 1
        Take into account for adjusted noise settings
         the further the parts we are sampling the greater the distance we have to smooth out for

        float noiseValue = (noise.Evaluate(point*settings.roughness + settings.centre)+1)* .5f;
        */
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            // So when roughness is greater than 1, the frequency will increase with each layer 

            float v = noise.Evaluate(point * frequency + settings.centre);
            noiseValue += (v + 1) * 0.5f * amplitude; // to make sure the range of amplitude is always between 0 and 1
            frequency *= settings.roughness;
            amplitude *= settings.persistance;

        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue*settings.strength;
    }

}
