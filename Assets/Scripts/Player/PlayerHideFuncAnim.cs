using UnityEngine;

public class PlayerHideFuncAnim : MonoBehaviour
{
    [SerializeField] private PlayerHideFunc hideFunc;

    public void SetHideStateNowhereInsideAnim() => hideFunc.hideState = PlayerHideFunc.HideState.Nowhere;
}