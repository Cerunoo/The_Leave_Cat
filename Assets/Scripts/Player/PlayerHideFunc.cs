using UnityEngine;
using TMPro;

public class PlayerHideFunc : MonoBehaviour
{
    // Нигде, Дом, Куст, Крыша, Яма
    public enum HideState { Nowhere, House, Bush, Roof, Pit }
    [SerializeField, Space(5)] private HideState hideState;

    [SerializeField] private TextMeshPro textState;

    public void SetHide(HideState state)
    {
        switch(state)
        {
            case HideState.Nowhere:
            hideState = HideState.Nowhere;
            break;

            case HideState.House:
            hideState = HideState.House;
            break;

            case HideState.Bush:
            hideState = HideState.Bush;
            break;

            case HideState.Roof:
            hideState = HideState.Roof;
            break;

            case HideState.Pit:
            hideState = HideState.Pit;
            break;
        }
        textState.text = hideState.ToString();
    }

    public HideState GetHide() => hideState;


    public static void SetHideInstance(HideState state)
    {
        PlayerController.Instance.GetComponent<PlayerHideFunc>().SetHide(state);
    }
    
    public static HideState GetHideInstance() => PlayerController.Instance.GetComponent<PlayerHideFunc>().hideState;
}