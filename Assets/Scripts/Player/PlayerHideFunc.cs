using UnityEngine;
using TMPro;

public class PlayerHideFunc : MonoBehaviour
{
    // Нигде, Дом, Куст, Крыша, Яма
    public enum HideState { Nowhere, House, Bush, Roof, Pit }
    [Space(5)] public HideState hideState;

    [SerializeField] private TextMeshPro textState;
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && hideState != HideState.Nowhere)
        {
            SetHide(HideState.Nowhere);
        } 
    }

    public void SetHide(HideState giveState)
    {
        if (Input.GetAxis("Horizontal") != 0)
        SwitchCase(HideState.Nowhere);
        else
        SwitchCase(giveState);

        void SwitchCase(HideState state)
        {
            if (hideState == state) return;

            switch(state)
            {
                case HideState.Nowhere:
                if (hideState == HideState.Bush || hideState == HideState.Pit)
                {
                    // Вызываеться из ключа аниматора Func PlayerHideFuncAnim.SetHideStateNowhereInsideAnim()
                    // hideState = HideState.Nowhere;
                    anim.SetTrigger("HideExit");
                }
                else
                {
                    hideState = HideState.Nowhere;
                }
                break;

                case HideState.House:
                hideState = HideState.House;
                break;

                case HideState.Bush:
                hideState = HideState.Bush;
                anim.SetTrigger("BushOrPit");
                anim.ResetTrigger("HideExit");
                break;

                case HideState.Roof:
                hideState = HideState.Roof;
                break;

                case HideState.Pit:
                hideState = HideState.Pit;
                anim.SetTrigger("BushOrPit");
                anim.ResetTrigger("HideExit");
                break;
            }
            textState.text = hideState.ToString();
        }
    }

    public HideState GetHide() => hideState;


    public static void SetHideInstance(HideState state)
    {
        PlayerController.Instance.GetComponent<PlayerHideFunc>().SetHide(state);
    }
    
    public static HideState GetHideInstance() => PlayerController.Instance.GetComponent<PlayerHideFunc>().hideState;
}