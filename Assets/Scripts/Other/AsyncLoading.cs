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

    [SerializeField] private Image fire;
    private Color fColor;

    private void Awake()
    {
        fColor = new Color();
        fColor.r = 0.01f;
        fColor.g = 0.01f;
        fColor.b = 0.01f;
        fColor.a = 1;

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
            fColor.r = Mathf.Clamp01(operation.progress / 0.9f);
            fColor.g = Mathf.Clamp01(operation.progress / 0.9f);
            fColor.b = Mathf.Clamp01(operation.progress / 0.9f);
            fire.color = fColor;
            progressText.text = " " + (progressSlider.value * 100).ToString("F0") + "%";
            yield return null;

            if (progressSlider.value == 1)
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    buttonActiveScene.interactable = true;

                    yield return new WaitForSecondsRealtime(0.4f);
                    StartCoroutine(animationProgressText());
                    IEnumerator animationProgressText()
                    {
                        yield return null;

                        string[] frames = new string[]{ "", "s", "sp", "spa", "spac", "space", "space ч", "space чт", "space что", "space чтоб", "space чтобы", "space чтобы н",
                        "space чтобы на", "space чтобы нач", "space чтобы нача", "space чтобы начат", "space чтобы начать", "space чтобы начать.", "space чтобы начать.."};

                        string step = "";
                        for (int i = 0; i < 12; i++)
                        {
                            foreach (string fr in frames)
                            {
                                progressText.text = step + fr;
                                yield return new WaitForSeconds(0.36f);
                            }
                            step += "";
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
