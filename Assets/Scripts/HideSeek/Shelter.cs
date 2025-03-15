using UnityEngine;
using static PlayerHideFunc;

public class Shelter : MonoBehaviour
{
    [SerializeField, Space(5)] private HideState hideType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetHideInstance(hideType);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetHideInstance(HideState.Nowhere);
        }
    }
}