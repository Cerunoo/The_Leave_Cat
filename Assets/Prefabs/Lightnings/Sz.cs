using UnityEngine;

public class Sz : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume *= SoundSaves.VfxSound / 100;
    }
}