using System.Collections.Generic;
using UnityEngine;

public class SoundSaves : MonoBehaviour
{
    public static SoundSaves Instance { get; private set; }

    private static float backSound;
    public static float BackSound
    {
        get => backSound;
        set
        {
            Instance = FindAnyObjectByType<SoundSaves>();

            if (value <= 100 && value >= 1)
            {
                backSound = value;
                PlayerPrefs.SetFloat("Back", value);
            }

            Instance.SetVolume();
        }
    }

    public bool initialize = false;
    public void SetVolume()
    {
        if (initialize != true)
        {
            pastVolume = new List<float>();
            allSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (AudioSource source in allSources)
            {
                pastVolume.Add(source.volume);
            }

            bgPastVolume = new List<float>();
            foreach (AudioSource source in bgSources)
            {
                bgPastVolume.Add(source.volume);
            }
            initialize = true;
        }

        for(int i = 0; i < allSources.Length; i++)
        {
            float res = pastVolume[i] * (VfxSound / 100);
            if (res > 1) res = 1;
            if (res < 0) res = 0;
            if (allSources[i] != null) allSources[i].volume = res;
        }

        for(int i = 0; i < bgSources.Length; i++)
        {
            float res = bgPastVolume[i] * (BackSound / 100);
            if (res > 1) res = 1;
            if (res < 0) res = 0;
            if (allSources[i] != null) bgSources[i].volume = res;
        }
    }

    private static float vfxSound;
    public static float VfxSound
    {
        get => vfxSound;
        set
        {
            Instance = FindAnyObjectByType<SoundSaves>();

            if (value <= 100 && value >= 1)
            {
                vfxSound = value;
                PlayerPrefs.SetFloat("VFX", value);
            }

            Instance.SetVolume();
        }
    }

    private void Start()
    {
        Instance = this;

        if (PlayerPrefs.HasKey("Back"))
        BackSound = PlayerPrefs.GetFloat("Back");
        else
        BackSound = 75;

        if (PlayerPrefs.HasKey("VFX"))
        VfxSound = PlayerPrefs.GetFloat("VFX");
        else
        VfxSound = 50;
    }
    public AudioSource[] allSources;
    public List<float> pastVolume;

    public AudioSource[] bgSources;
    public List<float> bgPastVolume;

    private void Update()
    {
        
    }
}
