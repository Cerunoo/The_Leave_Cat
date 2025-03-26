using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private GameObject misfortunePlayer;
    private Animator anim;
    private bool open;

    [SerializeField] private float holdQuit;
    [SerializeField] private Text quitText;

    [SerializeField] private Text backSound;
    [SerializeField] private Text vfxSound;

    [SerializeField] private Color selected;
    [SerializeField] private Color def;

    private int soundSaveSlot = 0;

    [SerializeField] private Text leftArrow;
    [SerializeField] private Text rightArrow;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (FindAnyObjectByType<HistoryController>().played) return;

        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!open)
            OpenPanel();
            else
            ClosePanel();
        }

        if (open && Input.GetKey(KeyCode.Escape))
        {
            holdQuit += Time.unscaledDeltaTime;
        }
        if (!Input.GetKey(KeyCode.Escape))
        {
            if (holdQuit > 0)
            {
                holdQuit -= Time.unscaledDeltaTime;
            }
        }

        if (holdQuit >= 0.4f)
        {
            quitText.fontSize = 70;
        }
        if (holdQuit >= 1)
        {
            quitText.fontSize = 75;
        }
        if (holdQuit >= 2.3f)
        {
            quitText.fontSize = 83;
        }
        if (holdQuit >= 3.5f)
        {
            quitText.fontSize = 90;
        }

        if (holdQuit < 0.4f)
        {
            quitText.fontSize = 63;
        }

        if (holdQuit >= 4)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

        backSound.text = "Фоновая музыка: " + SoundSaves.BackSound.ToString("F0");
        vfxSound.text = "Звуки: " + SoundSaves.VfxSound.ToString("F0");

        leftArrow.color = def;
        rightArrow.color = def;
        if (open && Input.GetKey(KeyCode.LeftArrow))
        {
            if (soundSaveSlot == 0)
            {
                SoundSaves.BackSound -= Time.unscaledDeltaTime * 15;
            }
            else
            {
                SoundSaves.VfxSound -= Time.unscaledDeltaTime * 15;
            }
            leftArrow.color = selected;
        }
        if (open && Input.GetKey(KeyCode.RightArrow))
        {
            if (soundSaveSlot == 0)
            {
                SoundSaves.BackSound += Time.unscaledDeltaTime * 15;
            }
            else
            {
                SoundSaves.VfxSound += Time.unscaledDeltaTime * 15;
            }
            rightArrow.color = selected;
        }

        if (open && Input.GetKey(KeyCode.UpArrow))
        {
            soundSaveSlot = 0;
        }
        if (open && Input.GetKey(KeyCode.DownArrow))
        {
            soundSaveSlot = 1;
        }

        backSound.color = def;
        vfxSound.color = def;
        if (soundSaveSlot == 0) backSound.color = selected;
        else vfxSound.color = selected;
    }

    public void OpenPanel()
    {
        anim.SetBool("show", true);
        Time.timeScale = 0f;
        open = true;

        misfortunePlayer.SetActive(true);

        backSound.color = def;
        vfxSound.color = def;
        backSound.color = selected;
        soundSaveSlot = 0;

        // Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel()
    {
        anim.SetBool("show", false);
        Time.timeScale = 1f;
        open = false;

        misfortunePlayer.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
    }
}
