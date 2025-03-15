using UnityEngine;

public class DangerNotify : MonoBehaviour
{
    [SerializeField] private Animator dangerRight;
    [SerializeField] private Animator dangerLeft;

    public void CallRightDanger()
    {
        dangerRight.SetTrigger("Danger");
    }

    public void CallLeftDanger()
    {
        dangerLeft.SetTrigger("Danger");
    }
}