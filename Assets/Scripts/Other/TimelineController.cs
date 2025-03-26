using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public static TimelineController Instance { get; private set; }

    public bool isPlay { get; private set; }
    
    private PlayableDirector director;

    private void Start()
    {
        Instance = this;

        director = GetComponent<PlayableDirector>();

        director.played += OnPlayed;
        director.stopped += OnStopped;
        if (director.playOnAwake) OnPlayed();
    }

    private void OnPlayed(PlayableDirector director = null)
    {
        DisableInteractions(true);
        isPlay = true;
    }

    private void OnStopped(PlayableDirector director)
    {
        DisableInteractions(false);
        isPlay = false;
    }

    public void PlayAsset()
    {
        director.Play();
    }

    public void PlayAsset(PlayableAsset asset)
    {
        director.Play(asset);
    }

    public void PlayAsset(PlayableAsset asset, DirectorWrapMode mode)
    {
        director.Play(asset, mode);
    }

    private void DisableInteractions(bool disable)
    {
        if (PlayerController.Instance != null) PlayerController.Instance.blockMove = disable;
    }

    public static void StaticDisableInteractions(bool disable)
    {
        if (Instance != null) Instance.DisableInteractions(disable);
    }
}
