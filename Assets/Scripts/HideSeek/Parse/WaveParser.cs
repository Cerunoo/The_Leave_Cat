using UnityEngine;
// using Newtonsoft.Json;

public class WaveParser
{
    public Wave[] GetWaves(string json)
    {
        WavesWrapper wrapper = JsonUtility.FromJson<WavesWrapper>(json);
        return wrapper.waves;
    }
}
