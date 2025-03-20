using UnityEngine;

public class LightTirgger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Inqu")
        {
            collision.GetComponent<CrowdPersonController>().ShowSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Inqu")
        {
            collision.GetComponent<CrowdPersonController>().HideSprite();
        }
    }
}
