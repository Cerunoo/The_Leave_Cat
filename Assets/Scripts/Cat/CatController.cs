using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float direction = 1;

    private Rigidbody2D rb;
    [SerializeField, Space(5)] Animator anim;

    private void Start()
    {
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
        }
    }

    private void Update()
    {
        if (direction == 0.5f)
        {
            anim.SetBool("walk", true);
            anim.SetBool("run", false);
        }
        else if (direction >= 0.7f)
        {
            anim.SetBool("run", true);
            anim.SetBool("walk", false);
        }
        else if (direction == 0)
        {
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * speed, 0) * Time.deltaTime;
    }
}