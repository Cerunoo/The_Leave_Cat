using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HistoryController : MonoBehaviour
{
    [SerializeField] private Image[] start;
    [SerializeField] private Image[] defeat;
    [SerializeField] private Image[] pass;

    [SerializeField] private Color def;
    [SerializeField] private Color black;
    [SerializeField] private Color black2;

    [SerializeField] Text notifySlider;

    public bool played;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Start"))
        {
            TimelineController.Instance.PlayAsset();
        }
        else
        {
            StartCoroutine(CheckStart());
        }
    }

    private IEnumerator CheckStart()
    {
        yield return StartCoroutine(SlideIterator(start));
        TimelineController.Instance.PlayAsset();
    }

    public IEnumerator CheckDefeat()
    {
        yield return StartCoroutine(SlideIterator(defeat, true));
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    // private IEnumerator CheckPass()
    // {
    //     yield return StartCoroutine(SlideIterator(pass));
    // }

    private IEnumerator SlideIterator(Image[] slides, bool noTransparent = false)
    {
        Time.timeScale = 0;
        played = true;

        for(int i = 0; i < slides.Length; i++)
        {
            slides[i].color = black2;
            slides[0].color = black;
            slides[i].gameObject.SetActive(true);
            while(def.r - slides[i].color.r > 0.015f)
            {
                slides[i].color = Color.Lerp(slides[i].color, def, Time.unscaledDeltaTime);
                yield return null;
            }
            if (i == 0) notifySlider.gameObject.SetActive(true);
            else notifySlider.GetComponent<Animator>().SetBool("blink", true);

            while(true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    notifySlider.GetComponent<Animator>().SetBool("blink", false);
                    break;
                }
                else yield return null;
            }

            if (i == slides.Length - 1)
            {
                if (noTransparent)
                {
                    while(slides[i].color.r - black.r > 0.015f)
                    {
                        slides[i].color = Color.Lerp(slides[i].color, black, Time.unscaledDeltaTime);
                        yield return null;
                    }
                }
                else
                {
                    foreach(Image slide in slides)
                    {
                        notifySlider.gameObject.SetActive(false);
                        slide.gameObject.SetActive(false);
                    }
                }
            }

            yield return null;
        }

        PlayerPrefs.SetInt("Start", 1);

        Time.timeScale = 1;
        played = false;
    }
}