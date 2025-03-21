using UnityEngine;

public class PlayerHideFuncAnim : MonoBehaviour
{
    [SerializeField] private PlayerHideFunc hideFunc;

    public void SetHideStateNowhereInsideAnim()
    {
        hideFunc.hideState = PlayerHideFunc.HideState.Nowhere;
        hideFunc.animHideState = PlayerHideFunc.HideState.Nowhere;
    }

    public void SetHideStateHouseInsideAnim() => hideFunc.animHideState = PlayerHideFunc.HideState.House;
    public void SetHideStateBushInsideAnim() => hideFunc.animHideState = PlayerHideFunc.HideState.Bush;
    public void SetHideStateRoofInsideAnim() => hideFunc.animHideState = PlayerHideFunc.HideState.Roof;
    public void SetHideStatePitInsideAnim() => hideFunc.animHideState = PlayerHideFunc.HideState.Pit;

    public void DontMovePlayer() => PlayerController.Instance.blockMove = true;
    public void MovePlayer() => PlayerController.Instance.blockMove = false;
}