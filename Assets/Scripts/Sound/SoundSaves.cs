using UnityEngine;

public class SoundSaves : MonoBehaviour
{
    private static float backSound;
    public static float BackSound
    {
        get => backSound;
        set
        {
            if (value <= 100 && value >= 0)
            {
                backSound = value;
                PlayerPrefs.SetFloat("Back", value);
            }
        }
    }

    private static float vfxSound;
    public static float VfxSound
    {
        get => vfxSound;
        set
        {
            if (value <= 100 && value >= 0)
            {
                vfxSound = value;
                PlayerPrefs.SetFloat("VFX", value);
            }
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Back"))
        BackSound = PlayerPrefs.GetFloat("Back");
        else
        BackSound = 75;

        if (PlayerPrefs.HasKey("VFX"))
        VfxSound = PlayerPrefs.GetFloat("VFX");
        else
        VfxSound = 50;
    }

    private void Update()
    {
        
    }
}
