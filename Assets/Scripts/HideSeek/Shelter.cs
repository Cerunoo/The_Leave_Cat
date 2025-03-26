using UnityEngine;
using static PlayerHideFunc;
using System.Collections;

public class Shelter : MonoBehaviour
{
    [Space(5)] public HideState hideType;

    [SerializeField, Space(5)] private SpriteRenderer sprite;
    [SerializeField] private Material defGirl;
    [SerializeField] private Material selectedGirl;

    [SerializeField, Space(5)] float delayHide = 0.5f;

    private AudioSource sz;

    private bool girlInside;
    public IEnumerator ResetGirlInside()
    {
        yield return new WaitForSeconds(delayHide);
        girlInside = false;
    }

    private PlayerHideFunc hideFunc;

    private void Start()
    {
        hideFunc = PlayerController.Instance.GetComponent<PlayerHideFunc>();

        sz = GetComponent<AudioSource>();
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

            sz.Play();
        }
    }

    public bool CheckGirl()
    {
        return girlInside;
    }
}