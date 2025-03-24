using UnityEngine;
using System.Collections;
using static PlayerHideFunc;

public class CrowdPersonController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    private ICrowdController _parent;
    private CrowdController parentObject;

    [SerializeField] private Collider2D solidCollider;
    public LayerMask toRightInquPreset;
    public LayerMask toLeftInquPreset;

    [SerializeField, Space(5)] private float speedDivergenceTime;
    private float koafSpeedMyIndexQueue;
    private float startKoafSpeedMyIndexQueue;

    [SerializeField, Space(5)] private Color hideColor;
    [SerializeField] private Color showColor;
    [SerializeField] private float speedShowSprite;
    private SpriteRenderer sprite;

    public delegate void SeekTrigger(CrowdPersonController person);
    private SeekTrigger seekTrigger;

    private float koafSlow = 1;

    private HideState myHideCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Инициализировать родителя при Instantiate
    public void InitializeParent(ICrowdController parent, CrowdController gObject, LayerMask myMoveType, float speedQueue, float speedQueueBase)
    {
        _parent = parent;
        parentObject = gObject;

        if (myMoveType == parentObject.toRightInqu)
        {
            solidCollider.excludeLayers = toRightInquPreset;
            gameObject.layer = LayerMask.NameToLayer("ToRightInqu");
        }
        else
        {
            solidCollider.excludeLayers = toLeftInquPreset;
            gameObject.layer = LayerMask.NameToLayer("ToLeftInqu");
        }

        koafSpeedMyIndexQueue = speedQueue;
        startKoafSpeedMyIndexQueue = speedQueueBase;
    }

    public void ChooseTrigger(SeekTrigger seekVoid = null)
    {
        seekTrigger = seekVoid;
    }

    public void InitializeHideCheck(HideState giveHideCheck)
    {
        myHideCheck = giveHideCheck;

        if (myHideCheck != HideState.House) sprite.color = hideColor;
    }

    public void InitializeWeapon(string typeSearch)
    {
        anim.SetTrigger(typeSearch);
    }

    private void FixedUpdate()
    {
        koafSpeedMyIndexQueue = Mathf.MoveTowards(koafSpeedMyIndexQueue, startKoafSpeedMyIndexQueue, speedDivergenceTime * Time.deltaTime);

        if (_parent != null)
        rb.linearVelocity = new Vector2(_parent.Speed * _parent.Direction * _parent.SpeedCurve * koafSlow * koafSpeedMyIndexQueue * 30, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            parentObject.CheckPlayer();
        }

        if (collision.tag == "Shelter" && parentObject.CanCheckShelter(collision.GetComponent<Shelter>()))
        {
            koafSlow = 0.5f;
            if (seekTrigger != null && CanCheckShelterForMyType(collision.GetComponent<Shelter>()))
            {
                seekTrigger(this);
            }

            parentObject.StartCheckShelter(collision.GetComponent<Shelter>());
        }
    }

    private bool CanCheckShelterForMyType(Shelter shelter)
    {
        if (shelter.hideType == myHideCheck)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            parentObject.CheckPlayer();
        }

        if (collision.tag == "Shelter" && parentObject.CanCheckShelter(collision.GetComponent<Shelter>()))
        {
            parentObject.StartCheckShelter(collision.GetComponent<Shelter>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shelter" && parentObject.CanCheckShelter(collision.GetComponent<Shelter>()))
        {
            koafSlow = 1;
        }
    }

    public void HideSprite()
    {
        // if (gameObject.activeInHierarchy) StartCoroutine(Transparency(hideColor));
    }

    public void ShowSprite()
    {
        if (gameObject.activeInHierarchy) StartCoroutine(Transparency(showColor));
    }

    private IEnumerator Transparency(Color color)
    {
        while (sprite.color.a != color.a)
        {
            Color updateColor = sprite.color;
            updateColor.a = Mathf.MoveTowards(updateColor.a, color.a, speedShowSprite * Time.deltaTime);

            sprite.color = updateColor;

            yield return null;
        }
    }
}
