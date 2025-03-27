using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float direction = 1;

    private Rigidbody2D rb;
    [SerializeField, Space(5)] Animator anim;

    [SerializeField] private float minDistance1;
    [SerializeField] private float minDistance2;
    [SerializeField] private float distanceMulti;

    private void Start()
    {
        kon = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // direction = 0;
            PlayerController.Instance.blockMove = true;
            StartCoroutine(FindAnyObjectByType<HistoryController>().CheckPass());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TriggerCat")
        {
            direction = 0;
            distanceMulti = 0;
            kon = true;
        }
    }
    private bool kon;

    private void Update()
    {
        if (direction == 0.5f || distanceMulti == 0.8f)
        {
            if (distanceMulti == 0)
            {
                anim.SetBool("run", false);
                anim.SetBool("walk", false);
                return;
            }
            anim.SetBool("walk", true);
            anim.SetBool("run", false);
        }
        else if (direction >= 0.7f || distanceMulti == 3f || distanceMulti == 1.5f)
        {
            if (distanceMulti == 0)
            {
                anim.SetBool("run", false);
                anim.SetBool("walk", false);
                return;
            }
            anim.SetBool("run", true);
            anim.SetBool("walk", false);
        }
        else if (direction == 0 || distanceMulti == 0)
        {
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
        }

        if (distanceMulti == 0)
        {
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
        }
    }

    float multi6;
    private void FixedUpdate()
    {
        if (kon)
        {
            distanceMulti = 0;
        }

        distanceMulti = 1;
        float dist = Mathf.Abs(FindAnyObjectByType<PlayerController>().transform.position.x - transform.position.x);
        if (dist > minDistance1)
        {
            distanceMulti *= 0f;
        }
        else if (dist > minDistance2)
        {
            distanceMulti *= 0.8f;
        }
        else if (dist < minDistance2)
        {
            distanceMulti *= 3f;
            multi6 = 15f;
        }
        else if (dist < minDistance1)
        {
            distanceMulti *= 1.5f;
        }

        if (multi6 > 0)
        {
            multi6 -= Time.deltaTime;
            distanceMulti = 3f;
        }

        if (kon)
        {
            distanceMulti = 0;
        }

        rb.linearVelocity = new Vector2(direction * speed * Time.deltaTime * distanceMulti, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(FindAnyObjectByType<PlayerController>().transform.position.x, transform.position.y), transform.position);
    }
}