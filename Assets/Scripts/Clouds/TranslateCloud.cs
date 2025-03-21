using UnityEngine;

public class TranslateCloud : MonoBehaviour
{
    [SerializeField] private Vector2 scaleRange;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private Vector2 transparentRange;

    private float speed;

    private void Start()
    {
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = new Vector2(scale, scale);

        speed = Random.Range(speedRange.x, speedRange.y);

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color newColor = sprite.color;
        newColor.a = Random.Range(transparentRange.x, transparentRange.y);
        sprite.color = newColor;
    }

    private void Update()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, 0));
    }
}
