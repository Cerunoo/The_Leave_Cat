using UnityEngine;

public class DangerNotify : MonoBehaviour
{
    public static DangerNotify Instance { get; private set; }

    [SerializeField] private Animator dangerRight;
    [SerializeField] private Animator dangerLeft;

    private void Awake()
    {
        Instance = this;
    }

    public void CallRightDanger()
    {
        dangerRight.SetTrigger("Danger");
        dangerRight.GetComponent<AudioSource>().Play();
    }

    public void CallLeftDanger()
    {
        dangerLeft.SetTrigger("Danger");
        dangerLeft.GetComponent<AudioSource>().Play();
    }

    // Вызов от Player
    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}