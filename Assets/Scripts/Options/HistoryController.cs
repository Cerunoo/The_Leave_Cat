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

    private AudioSource source;
    [SerializeField] private AudioClip startBG;
    [SerializeField] private AudioClip defBG;
    [SerializeField] private AudioClip passBG;
    [SerializeField] private AudioClip myaPAss;
    [SerializeField] private AudioClip listPages;

    [SerializeField] private GameObject dust;

    private void Start()
    {
        source = GetComponent<AudioSource>();

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
        OffSound();
        source.clip = startBG;
        source.Play();

        PlayerPrefs.SetInt("Start", 1);
        yield return StartCoroutine(SlideIterator(start));
        TimelineController.Instance.PlayAsset();

        ReturnSound();
    }

    public IEnumerator CheckDefeat()
    {
        if (passed) yield break;

        // OffSound();
        source.clip = defBG;
        source.Play();

        yield return StartCoroutine(SlideIterator(defeat, true));
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private bool passed = false;
    public IEnumerator CheckPass()
    {
        passed = true;

        OffSound();
        // source.clip = passBG;
        source.Play();
        source.PlayOneShot(myaPAss);
        StartCoroutine(wait());
        IEnumerator wait()
        {
            yield return new WaitForSecondsRealtime(1f);
            while(source.volume != 0)
            {
                source.volume = Mathf.Lerp(source.volume, 0, 2.5f * Time.unscaledDeltaTime);
                yield return null;
            }
        }

        yield return StartCoroutine(SlideIterator(pass, false, true, true));
        PlayerPrefs.DeleteAll();
    }

    private void OffSound()
    {
        AudioSource[] allSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (AudioSource source in allSources)
        {
            source.enabled = false;
        }
        source.enabled = true;
    }
    private void ReturnSound()
    {
        AudioSource[] allSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (AudioSource source in allSources)
        {
            source.enabled = true;
        }
        StartCoroutine(trans());
        IEnumerator trans()
        {
            while(source.volume != 0)
            {
                source.volume = Mathf.Lerp(source.volume, 0, 2.5f * Time.unscaledDeltaTime);
                yield return null;
            }
        }
    }

    private IEnumerator SlideIterator(Image[] slides, bool noTransparent = false, bool block = false, bool pass = false)
    {
        dust.SetActive(true);

        Time.timeScale = 0;
        played = true;

        for(int i = 0; i < slides.Length; i++)
        {
            slides[i].color = black2;
            slides[0].color = black;
            slides[i].gameObject.SetActive(true);
            if (pass && i == slides.Length - 1)
            {
                source.enabled = false;
            }
            while(def.r - slides[i].color.r > 0.015f)
            {
                slides[i].color = Color.Lerp(slides[i].color, def, Time.unscaledDeltaTime);
                yield return null;
            }
            if (i == 0) notifySlider.gameObject.SetActive(true);
            else notifySlider.GetComponent<Animator>().SetBool("blink", true);
            if (i == slides.Length - 1 && block) notifySlider.GetComponent<Animator>().SetBool("blink", false);

            while(true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    source.PlayOneShot(listPages);
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
                else if (!block)
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

        Time.timeScale = 1;
        played = false;

        dust.SetActive(false);
    }
}