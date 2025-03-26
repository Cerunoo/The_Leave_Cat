using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsyncLoading : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Text progressText;
    [SerializeField] private Button buttonActiveScene;

    [SerializeField] private float timeToLoad;
    
    private AsyncOperation operation;

    private void Awake()
    {
        Screen.fullScreen = true;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(LoadAsync(1));
        }
    }

    private void Update()
    {
        if (buttonActiveScene.interactable == true)
        {
            if (Input.GetKey(KeyCode.Space)) pressActiveScene();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }

    public IEnumerator LoadAsync(int indexScene)
    {
        // if (SceneManager.GetActiveScene().buildIndex != 0)
        // {
        //     Button[] allButton = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        //     for (int i = 0; i < allButton.Length; i++) allButton[i].interactable = false;
        //     GetComponent<Animator>().SetBool("Show", true);
        // }
        
        yield return new WaitForSeconds(timeToLoad);

        operation = SceneManager.LoadSceneAsync(indexScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progressSlider.value = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = " " + (progressSlider.value * 100).ToString("F0") + "%";
            yield return null;

            if (progressSlider.value == 1)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    buttonActiveScene.interactable = true;

                    StartCoroutine(animationProgressText());
                    IEnumerator animationProgressText()
                    {
                        yield return new WaitForSeconds(0.9f);

                        string[] frames = new string[]{ "", "p", "pr", "pre", "pres", "press", "press s", "press sp", "press spa", "press spac", "press spac", "press space",
                        "press space t", "press space to", "press space to s", "press space to st", "press space to sta", "press space to star", "press space to start", "press space to start/", "press space to start.. //"};

                        string step = "  ";
                        for (int i = 0; i < 12; i++)
                        {
                            foreach (string fr in frames)
                            {
                                progressText.text = step + fr;
                                yield return new WaitForSeconds(0.36f);
                            }
                            step += "   ";
                        }
                    }
                }
                // else
                // {
                //     operation.allowSceneActivation = true;
                // }
                break;
            }
        }
    }
    public void pressActiveScene() { operation.allowSceneActivation = true; }
}
