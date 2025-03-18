using UnityEngine;
using static PlayerHideFunc;

public class Shelter : MonoBehaviour
{
    [Space(5)] public HideState hideType;

    [SerializeField, Space(5)] private SpriteRenderer sprite;
    [SerializeField] private Material defGirl;
    [SerializeField] private Material selectedGirl;

    private bool girlInside;

    private void Update()
    {
        if (girlInside && PlayerController.Instance.GetComponent<PlayerHideFunc>().hideState == hideType)
        sprite.material = selectedGirl;
        else
        sprite.material = defGirl;
    }

    private void OnTriggerStay2D(Collider2D collision)
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