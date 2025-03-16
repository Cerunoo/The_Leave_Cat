using UnityEngine;
using static PlayerHideFunc;

public class Shelter : MonoBehaviour
{
    [Space(5)] public HideState hideType;

    private bool girlInside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetHideInstance(hideType);
            girlInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetHideInstance(HideState.Nowhere);
            girlInside = false;
        }
    }

    public bool CheckGirl()
    {
        return girlInside;
    }
}