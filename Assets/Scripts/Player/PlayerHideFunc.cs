using UnityEngine;
using TMPro;

public class PlayerHideFunc : MonoBehaviour
{
    // Нигде, Дом, Куст, Крыша, Яма
    public enum HideState { Nowhere, House, Bush, Roof, Pit }
    [Space(5)] public HideState hideState;
    public HideState animHideState;

    [SerializeField] private TextMeshPro textState;
    [SerializeField] private Animator anim;

    private Shelter onlyShelter;

    private AudioSource sz;
    [SerializeField, Space(5)] private AudioClip[] screamers;

    private void Start()
    {
        sz = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && hideState != HideState.Nowhere)
        {
            SetHide(HideState.Nowhere, null);
        } 
    }

    public void SetHide(HideState giveState, Shelter giveShelter)
    {
        if (onlyShelter != null && giveState == HideState.Nowhere)
        StartCoroutine(onlyShelter.ResetGirlInside());

        onlyShelter = giveShelter;

        switch(giveState)
        {
            case HideState.Nowhere:
            // Вызываеться из ключа аниматора Func PlayerHideFuncAnim.SetHideStateNowhereInsideAnim()
            // hideState = HideState.Nowhere;
            anim.SetTrigger("HideExit");
            sz.Stop();
            break;

            case HideState.House:
            hideState = HideState.House;
            anim.SetTrigger("House");
            anim.ResetTrigger("HideExit");
            sz.PlayDelayed(1f);
            break;

            case HideState.Bush:
            hideState = HideState.Bush;
            anim.SetTrigger("Bush");
            anim.ResetTrigger("HideExit");
            sz.PlayOneShot(screamers[1]);
            break;

            case HideState.Roof:
            hideState = HideState.Roof;
            anim.SetTrigger("Roof");
            anim.ResetTrigger("HideExit");
            sz.PlayOneShot(screamers[1]);
            break;

            case HideState.Pit:
            hideState = HideState.Pit;
            anim.SetTrigger("Pit");
            anim.ResetTrigger("HideExit");
            sz.PlayOneShot(screamers[1]);
            break;
            }

            textState.text = hideState.ToString();
    }

    public HideState GetHide() => hideState;


    public static void SetHideInstance(HideState state, Shelter shelter)
    {
        PlayerController.Instance.GetComponent<PlayerHideFunc>().SetHide(state, shelter);
    }
    
    public static HideState GetHideInstance() => PlayerController.Instance.GetComponent<PlayerHideFunc>().hideState;
}