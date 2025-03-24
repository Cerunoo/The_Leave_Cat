using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    private Animator anim;

    private bool open;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!open)
            OpenPanel();
            else
            ClosePanel();
        }
    }

    public void OpenPanel()
    {
        anim.SetBool("show", true);
        Time.timeScale = 0f;
        open = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel()
    {
        anim.SetBool("show", false);
        Time.timeScale = 1f;
        open = false;

        Cursor.lockState = CursorLockMode.Locked;
    }
}
