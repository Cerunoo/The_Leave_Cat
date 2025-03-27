using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PassMelenger : MonoBehaviour
{
    [SerializeField] private Text esc;
    [SerializeField] private Text again;

    [SerializeField] private float esc1;
    [SerializeField] private float again1;

    private void OnEnable()
    {
        StartCoroutine(ExitCallback());
    }

    private void Update()
    {
        if (esc1 >= 2)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();

            esc1 = 0;
            again1 = 0;
        }

        else if (again1 >=2)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;

            again1 = 0;
            esc1 = 0;
        }
    }

    public IEnumerator ExitCallback()
    {
        while(true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                again1 += Time.unscaledDeltaTime;
            }
            else if (Input.GetKey(KeyCode.Escape))
            {
                esc1 += Time.unscaledDeltaTime;
            }

            if (!Input.GetKey(KeyCode.Space))
            {
                if (again1 > 0) again1 -= Time.unscaledDeltaTime;
            }
            if (!Input.GetKey(KeyCode.Escape))
            {
                if (esc1 > 0) esc1 -= Time.unscaledDeltaTime;
            }

            yield return null;
        }
    }
}
