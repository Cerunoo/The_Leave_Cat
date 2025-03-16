using UnityEngine;

public class WaveParser
{
    public Wave[] GetWaves(string json)
    {
        WavesWrapper wrapper = JsonUtility.FromJson<WavesWrapper>(json);
        return wrapper.waves;
    }
}

[System.Serializable]
public class WavesWrapper
{
    public Wave[] waves;
}