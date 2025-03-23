using UnityEngine;
using System.Collections;

public class LightingSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private GameObject wrapperPrefab;

    [SerializeField, Space(5)] private Vector2 delayRange;

    [SerializeField, Space(5)] private Vector2 radSpawnX;
    [SerializeField] private Vector2 radSpawnY;

    [SerializeField, Space(5)] private Transform playerPos;

    [SerializeField, Space(10)] private Vector2 randSize;
    [SerializeField, Space(5)] private Vector2 randRot;

    private void Start()
    {
        CallLight();
        CallLight();
        StartCoroutine(LightIterator());
        StartCoroutine(LightIterator());
        StartCoroutine(LightIterator());
    }

    private void CallLight()
    {
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(randRot.x, randRot.y));
        GameObject wrapper = Instantiate(wrapperPrefab, new Vector2(0, 0), rot);
        wrapper.transform.parent = transform;

        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        float posX = playerPos.position.x + Random.Range(radSpawnX.x, radSpawnX.y);
        float posY = playerPos.position.y + Random.Range(radSpawnY.x, radSpawnY.y);

        GameObject obj = Instantiate(prefab, new Vector2(0, 0), Quaternion.identity);
        Transform lighting = obj.transform;
        lighting.parent = wrapper.transform;
        wrapper.transform.position = new Vector2(posX, posY);

        float size = Random.Range(randSize.x, randSize.y);
        lighting.localScale = new Vector2(size, size);

        // В будущем еще 4 варианта сплеша
        lighting.GetComponent<Animator>().SetTrigger("Splash1");
        StartCoroutine(DestroyLighting());

        IEnumerator DestroyLighting()
        {
            yield return new WaitForSeconds(5f);
            Destroy(wrapper);
        }
    }

    private IEnumerator LightIterator()
    {
        float delay = Random.Range(delayRange.x, delayRange.y);
        yield return new WaitForSeconds(delay);

        CallLight();
        StartCoroutine(LightIterator());
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 from1 = new Vector2(playerPos.position.x + radSpawnX.x, playerPos.position.y + radSpawnY.x);
        Vector2 to1 = new Vector2(playerPos.position.x + radSpawnX.y, playerPos.position.y + radSpawnY.x);

        Vector2 from2 = new Vector2(playerPos.position.x + radSpawnX.x, playerPos.position.y + radSpawnY.y);
        Vector2 to2 = new Vector2(playerPos.position.x + radSpawnX.y, playerPos.position.y + radSpawnY.y);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(from1, to1);
        Gizmos.DrawLine(from2, to2);

        for (int i = 0; i < 50; i++) SupposePos();

        void SupposePos()
        {
            float posX = playerPos.position.x + Random.Range(radSpawnX.x, radSpawnX.y);
            float posY = playerPos.position.y + Random.Range(radSpawnY.x, radSpawnY.y);
            Vector2 center = new Vector2(posX, posY);
            float radius = 0.1f;

            Gizmos.color = Color.black;
            Gizmos.DrawSphere(center, radius);
        }
    }
}
