using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    private Animator anim;

    private bool open;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void ClosePanel()
    {
        anim.SetBool("show", false);
        Time.timeScale = 1f;
        open = false;
    }
}
