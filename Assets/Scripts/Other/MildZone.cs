using UnityEngine;

public class MildZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") PlayerController.Instance.isMild = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") PlayerController.Instance.isMild = false;
    }
}