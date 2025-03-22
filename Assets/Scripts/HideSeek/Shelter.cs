using UnityEngine;
using static PlayerHideFunc;
using System.Collections;

public class Shelter : MonoBehaviour
{
    [Space(5)] public HideState hideType;

    [SerializeField, Space(5)] private SpriteRenderer sprite;
    [SerializeField] private Material defGirl;
    [SerializeField] private Material selectedGirl;

    private bool girlInside;
    public IEnumerator ResetGirlInside()
    {
        yield return new WaitForSeconds(0.5f);
        girlInside = false;
        Debug.Log("");
    }

    private PlayerHideFunc hideFunc;

    private void Start()
    {
        hideFunc = PlayerController.Instance.GetComponent<PlayerHideFunc>();
    }

    private void Update()
    {
        if (girlInside && hideFunc.hideState == hideType && hideFunc.animHideState == hideType)
        sprite.material = selectedGirl;
        else
        sprite.material = defGirl;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetAxis("Horizontal") == 0 && !girlInside)
        {
            SetHideInstance(hideType, this);
            girlInside = true;
        }
    }

    public bool CheckGirl()
    {
        return girlInside;
    }
}