using UnityEngine;

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

    private float koafSlow = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            parentObject.CheckPlayer();
        }

        if (collision.tag == "Shelter" && parentObject.CanCheckShelter(collision.GetComponent<Shelter>()))
        {
            koafSlow = 0.5f;

            parentObject.StartCheckShelter(collision.GetComponent<Shelter>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Shelter")
        {
            koafSlow = 1;
        }
    }
}
