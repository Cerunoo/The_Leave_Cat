using UnityEngine;
using System.Collections;

public class CloudsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;

    [SerializeField, Space(5)] private Vector2 delayRange;
    [SerializeField] private Vector2 posRangeY;

    private int count = 0;

    private void Start()
    {
        SpawnCloud();
        StartCoroutine(SpawnIterator());
    }

    private void SpawnCloud()
    {
        GameObject prefab = clouds[Random.Range(0, clouds.Length)];
        float posY = Random.Range(posRangeY.x, posRangeY.y);

        Transform cloud = Instantiate(prefab, new Vector2(0, 0), Quaternion.identity).transform;
        cloud.parent = transform;
        cloud.localPosition = new Vector2(0, posY);

        count++;
        cloud.GetComponent<SpriteRenderer>().sortingOrder += count;
    }

    private IEnumerator SpawnIterator()
    {
        float delay = Random.Range(delayRange.x, delayRange.y);
        yield return new WaitForSeconds(delay);

        SpawnCloud();
        StartCoroutine(SpawnIterator());
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 from = new Vector2(transform.position.x, transform.position.y +  posRangeY.x);
        Vector2 to = new Vector2(transform.position.x, transform.position.y + posRangeY.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from, to);
    }
}
