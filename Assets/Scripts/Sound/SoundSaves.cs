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
            }
        }
    }

    private void Start()
    {
        BackSound = 75;
        VfxSound = 50;
    }

    private void Update()
    {
        
    }
}
